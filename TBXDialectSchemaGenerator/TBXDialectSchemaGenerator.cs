using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TBXDialectSchemaGenerator
{
    internal static class TBXDialectSchemaGenerator
    {
        private static readonly XNamespace SCH_NS = XNamespace.Get("http://purl.oclc.org/dsdl/schematron");

        private static KeyValuePair<string, IDictionary[]> HandleModuleRoot(string modulePath)
        {
            var modDoc = XDocument.Load(modulePath);
            var moduleRoot = modDoc.Root;
            if (moduleRoot == null) return default;

            string? key = moduleRoot.Attribute("module")?.Value;
            if (key == null) return default;
            var val = moduleRoot.Element("datCatSet")?
                .Elements()
                .Select(spec => new Dictionary<string, string?[]>() {
                    { "classificationElement", new string?[1] { Regex.Replace(spec.Name.LocalName, "Spec$", "") }},
                    { "datcat", new string?[1] { spec.Attribute("name")?.Value } },
                    { "datatype",
                        new string?[1] {
                            spec.Element("contents")?
                            .Attribute("datatype")?.Value
                        }
                    },
                    { "picklist",
                        spec.Descendants("value")
                        .Where(v => v != null)
                        .Select(v => v.Value)
                        .ToArray()
                    },
                    { "levels",
                        spec.Descendants("level")
                        .Where(v => v != null)
                        .Select(v => v.Value)
                        .ToArray()
                    }});
            if (val?.Any() != true) return default;
            return new KeyValuePair<string, IDictionary[]>(key, val.ToArray());
        }

        private static Dictionary<string, IDictionary[]> GetDataFromModules(IEnumerable<string> modules)
        {
            return modules?.Select(HandleModuleRoot)
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value) ?? new Dictionary<string, IDictionary[]>();
        }


        private static readonly XElement CoreEnforcement = new XElement(SCH_NS + "pattern",
            new XAttribute("id", "coreEnforcement"),
            new XElement(SCH_NS + "rule",
                new XAttribute("context", "tbx:termNote"),
                new XElement(SCH_NS + "assert",
                    new XAttribute("test",
                        "parent::tbx:termSec or parent::tbx:termNoteGrp/parent::tbx:termSec"),
                    "Any termNote is only allowed at the termSec level.")),
            new XElement(SCH_NS + "rule",
                new XAttribute("context", "tbx:*[@type]"),
                new XElement(SCH_NS + "assert",
                    new XAttribute("test", "@type != ''"),
                    "Data category must be declared.  If no permitted data categories are listed in the grammar schema, blank values are also not allowed.")),
            new XElement(SCH_NS + "rule",
                new XAttribute("context", "*[@target]"),
                new XElement(SCH_NS + "assert",
                    new XAttribute("test",
                        "matches(@target,'https?://.+') or @target = //*/@id"),
                    "ID must be IDREF for internal references or URI following HTTP protocol for external references."))
            );

        private static readonly XElement XliffInlineConstraints = new XElement(SCH_NS + "pattern",
            new XAttribute("id", "XLIFF.inlineConstraints"),
            new XElement(SCH_NS + "rule",
                new XAttribute("context", "tbx:sc[following-sibling::tbx:ec]"),
                new XElement(SCH_NS + "assert",
                    new XAttribute("test",
                        "@isolated='no' or not(@isolated)"),
                    "@isolated must be 'no' if <sc/> or <ec/> has its corresponding <sc/>/<ec/> in the same note text and @startRef must be used for <ec>")),
            new XElement(SCH_NS + "rule",
                new XAttribute("context", "tbx:ec[preceding-sibling::tbx:sc]"),
                new XElement(SCH_NS + "assert",
                    new XAttribute("test", "@isolated='no' or not(@isolated)"),
                    "@isolated must be 'no' if <sc/> or <ec/> has its corresponding <sc/>/<ec/> in the same note text and @startRef must be used for <ec>"),
                new XElement(SCH_NS + "assert",
                    new XAttribute("test", "@startRef"),
                    "@starRef is required for <ec> if it is in the same note text as its corresponding <sc>"),
                new XComment(
                    new XElement("assert",
                        new XAttribute("test", "not(@dir)"),
                        "@dir only permitted when @isolated is 'yes'.")
                    .ToString()),
                new XComment("@dir IS NOT CURRENTLY USED IN TBX")),
            new XElement(SCH_NS + "rule",
                new XAttribute("context",
                    "tbx:sc[not(following-sibling::tbx:ec)]"),
                new XElement(SCH_NS + "assert",
                    new XAttribute("test", "@isolated='yes' or not(@isolated)"),
                        "@isolated must be 'yes' if <sc/> or <ec/> does not have its corresponding <sc/>/<sc/> in the same note text.")),
            new XElement(SCH_NS + "rule",
                new XAttribute("context", "tbx:ec[not(preceding-sibling::tbx:sc)]"),
                new XElement(SCH_NS + "assert",
                    new XAttribute("test",
                        "@isolated='yes' or not(@isolated)"),
                    "@isolated must be 'yes' if <sc/> or <ec/> does not have its corresponding <sc/>/<sc/> in the same note text."),
                new XElement(SCH_NS + "assert",
                    new XAttribute("test", "@id"),
                    "@id is REQUIRED when @isolated is or should be 'yes'.")),
            new XElement(SCH_NS + "rule",
                new XAttribute("context", "tbx:ec[@isolated='yes']"),
                new XElement(SCH_NS + "assert",
                    new XAttribute("test", "@id != ''"),
                    "ID is required if @isolated is 'yes'.")));
    
        private static XElement GenerateDialectEnforcement(string dialect, IDictionary<string, IDictionary[]> moduleData)
        {
            var moduleMap = moduleData.Values
                .SelectMany(dicts => dicts)
                .OfType<Dictionary<string, string?[]>>()
                .GroupBy(dict =>
                    dict.TryGetValue("classificationElement", out var classificationElementArray) ? classificationElementArray.FirstOrDefault() : null);
            var pattern = new XElement(SCH_NS + "pattern",
                new XAttribute("id", "dialectEnforcement"),
                new XElement(SCH_NS + "rule",
                    new XAttribute("context", "tbx:tbx"),
                    new XElement(SCH_NS + "assert",
                        new XAttribute("test", $"attribute::type='{dialect}'"),
                        $"The name of this dialect should be {dialect}."),
                    new XElement(SCH_NS + "assert",
                        new XAttribute("test", "attribute::style='dca'"),
                        "The style of this dialect should be declared as 'dca'.")),
                new XElement(SCH_NS + "rule",
                    new XAttribute("context", "*[not(namespace-uri() = 'urn:iso:std:iso:30042:ed-2')]"),
                    new XElement(SCH_NS + "assert",
                        new XAttribute("test", "false()"),
                        "DCT style elements are not permitted in DCA style TBX.")),
                new XComment(" Data Category Types "));

            foreach (var classificationGroup in moduleMap.OrderBy(group => group.Key))
            {
                var datcats = classificationGroup.Select(dict => dict.TryGetValue("datcat", out var dcArray) ? dcArray.FirstOrDefault() : null);
                var test = string.Join(" or ", datcats.Select(dc => $".='{dc}'"));

                pattern.Add(new XElement(SCH_NS + "rule",
                    new XAttribute("context",
                        $"tbx:{classificationGroup.Key}/@type"),
                    new XElement(SCH_NS + "assert",
                        new XAttribute("test", test),
                        $"Permitted type value(s): {string.Join(", ", datcats)}")));
            }
            return pattern;
        }

        private static IEnumerable<XObject> GenerateModuleEnforcement(IDictionary<string, IDictionary[]> moduleData)
        {
            var moduleEnforcementClauses = new List<XObject>();
            foreach (var (moduleName, moduleContent) in moduleData)
            {
                moduleEnforcementClauses.Add(new XComment($" {moduleName} module rules "));
                var classificationElementGroups = moduleContent.OfType<Dictionary<string, string?[]>>()
                    .GroupBy(dict => dict.TryGetValue("classificationElement", out var classificationElementArray) ? classificationElementArray.FirstOrDefault() : null);

                var patterns = new List<XElement>();
                foreach (var (classificationElement, moduleInfos) in classificationElementGroups.Select(group => (group.Key, group.ToList())))
                {
                    List<XElement> rules = new List<XElement>();
                    foreach (var moduleInfo in moduleInfos)
                    {
                        moduleInfo.TryGetValue("datcat", out var datcatArray);
                        if (datcatArray == null) continue;
                        string? datcat = datcatArray.FirstOrDefault();

                        IEnumerable<string> tests = new List<string>();
                        var levels = moduleInfo["levels"];
                        switch (classificationElement)
                        {
                            case "ref":
                                tests = levels.Select(l =>
                                            $"parent::tbx:{l} or parent::tbx:*[contains(./local-name(),'Grp')]/parent::tbx:{l}");
                                break;
                            case "admin":
                                tests = levels.Select(l =>
                                            $"parent::tbx:{l} or parent::tbx:*[contains(./local-name(),'Grp')][not(./local-name() = 'transacGrp')]/parent::tbx:{l}");
                                break;
                            case "transacNote":
                                tests = levels.Select(l =>
                                            $"parent::tbx:{l} or parent::tbx:transacGrp/parent::tbx:{l}");
                                break;
                            case var c when Regex.IsMatch(c ?? "", @"^(descrip|transac|termNote)$"):
                                tests = levels.Select(l =>
                                            $"parent::tbx:{l} or parent::tbx:{c}Grp/parent::tbx:{l}");
                                break;
                            default:
                                tests = levels.Select(l => $"parent::tbx:{l}");
                                break;
                        }

                        string test = string.Join(" or ", tests);

                        if (levels.Any())
                        {
                            rules.Add(new XElement(SCH_NS + "rule",
                                new XAttribute("context", $"tbx:{classificationElement}[@type='{datcat}']"),
                                new XElement(SCH_NS + "assert",
                                    new XAttribute("test", test),
                                        $"/{datcat}/ may only appear at level(s): {string.Join(", ", levels.Select(l => l))}")));
                        }
                        if (moduleInfo.TryGetValue("datatype", out var datatypeArray) && datatypeArray.FirstOrDefault() == "picklist"
                            && moduleInfo.TryGetValue("picklist", out var picklist))
                        {
                            rules.Add(
                                new XElement(SCH_NS + "rule",
                                    new XAttribute("context", $"tbx:{classificationElement}[@type='{datcat}']"),
                                    new XElement(SCH_NS + "assert",
                                        new XAttribute("test", string.Join(" or ", picklist.Select(p => $".='{p}'"))),
                                        $"/{datcat}/ type may be: {string.Join(" or ", picklist.Select(p => $"'{p}'"))}"))
                            );
                        }

                    }

                    patterns.Add(new XElement(SCH_NS + "pattern", new XAttribute("id", $"module.{moduleName}.{classificationElement}"), rules));
                }
                patterns.Sort((p1, p2) => string.Compare(p1.Attribute("id")?.Value, p2.Attribute("id")?.Value));
                moduleEnforcementClauses.AddRange(patterns);
            }
            return moduleEnforcementClauses;
        }

        private static XDocument BuildSchema(string dialect, Dictionary<string, IDictionary[]> moduleData)
        {
            return new XDocument(
                new XElement(SCH_NS + "schema",
                     new XAttribute("queryBinding", "xslt2"),
                     new XAttribute(XNamespace.Xmlns + "sqf", "http://www.schematron-quickfix.com/validator/process"),
                     new XElement(SCH_NS + "ns", 
                        new XAttribute("uri", "urn:iso:std:iso:30042:ed-2"),
                        new XAttribute("prefix", "tbx")),
                        CoreEnforcement,
                        XliffInlineConstraints,
                        GenerateDialectEnforcement(dialect, moduleData),
                        GenerateModuleEnforcement(moduleData)));
        }

        public static string Generate(string dialect, params string[] modules)
        {
            using (Utf8StringWriter sw = new Utf8StringWriter())
            {
                BuildSchema(dialect, GetDataFromModules(modules)).Save(sw);
                return sw.ToString();
            }
        }

        private class Utf8StringWriter : StringWriter
        {
            public override Encoding Encoding => Encoding.UTF8;
        }
    }
}
