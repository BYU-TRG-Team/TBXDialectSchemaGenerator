using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
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
        private const string SCH_NS = "http://purl.oclc.org/dsdl/schematron";

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
            return new KeyValuePair<string, IDictionary[]>(key, val as IDictionary[]);
        }

        private static Dictionary<string, IDictionary[]>? GetDataFromModules(IEnumerable<string> modules)
        {
            return modules?.Select(HandleModuleRoot)
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
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
                    new XAttribute("test", "@startRef"),
                    "@starRef is required for <ec> if it is in the same note text as its corresponding <sc>"),
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
                    new XAttribute("test",
                        "@isolated must be 'yes' if <sc/> or <ec/> does not have its corresponding <sc/>/<sc/> in the same note text."))),
            new XElement(SCH_NS + "rule",
                new XAttribute("test",
                    "@isolated='yes' or not(@isolated)"),
                new XElement(SCH_NS + "assert",
                    new XAttribute("test", "@id"),
                    "@id is REQUIRED when @isolated is or should be 'yes'.")),
            new XElement(SCH_NS + "rule",
                new XAttribute("context", "tbx:ec[@isolated='yes']"),
                new XElement(SCH_NS + "assert",
                    new XAttribute("test", "@id != ''"),
                    "ID is required if @isolated is 'yes'.")));
    
        private static XElement GenerateDialectEnforcement(string dialect, Dictionary<string, IDictionary[]> moduleData)
        {
            var moduleMap = moduleData.Values
                .SelectMany(dicts => dicts)
                .GroupBy(dict => dict["classificationElement"]);
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

            foreach (var () in moduleMap.OrderBy(dict => dict.First()["classificationElement"]))
            return default;
        }

        private static XDocument BuildSchema(string dialect, Dictionary<string, IDictionary[]> moduleData)
        {
            return new XDocument(new XDeclaration("1.0", "utf-8", null),
                new XElement(SCH_NS + "schema",
                     new XAttribute("queryBinding", "xslt2"),
                     new XAttribute(XNamespace.Xmlns + "sqf", "http://www.schematron-quickfix.com/validator/process"),
                     new XElement(SCH_NS + "ns", 
                        new XAttribute("uri", "urn:iso:std:iso:30042:ed-2"),
                        new XAttribute("prefix", "tbx")),
                        CoreEnforcement,
                        XliffInlineConstraints,
                        GenerateDialectEnforcement(dialect, moduleData),

                ));

        }

        string Generate(string dialect, params string[] modules)
        {
            var moduleData = GetDataFromModules(modules);
             
        }
    }
}
