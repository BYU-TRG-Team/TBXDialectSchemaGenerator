using clojure.clr.api;
using clojure.lang;
using System.CodeDom.Compiler;
using System.Collections;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks.Sources;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace TBXDialectSchemaGenerator
{
    public partial class TBXDialectSchemaGeneratorForm : Form
    {
        private class TBXMDItem : IComparable<TBXMDItem>  
        {
            public string Text {
                get => ResourceType == EResourceType.Internal ?
                    $"{Name} Module (internal)"
                    : $"{Name} - {Resource}";
            }

            public enum EResourceType
            {
                Internal,
                External
            }

            public string Name { get; private set; }
            private string? _tempPath = null;
            private string? _resource = null;
            public string Resource {
                get
                {
                    if (_resource == null) return "";

                    if (ResourceType == EResourceType.External)
                    {
                        return _resource;
                    }
                    else if (string.IsNullOrWhiteSpace(_tempPath) || !File.Exists(_tempPath))
                    {
                        _tempPath = Path.Join(
                            Path.GetTempPath(),
                            Path.GetRandomFileName()
                        );
                        byte[]? tbxmd = Schemas.ResourceManager.GetObject(_resource) as byte[];
                        if (tbxmd == null) throw new FileNotFoundException("Temporary resource for internal module could not be loaded.");
                        File.WriteAllBytes(_tempPath, tbxmd);
                    }

                    return _tempPath;
                }
                private set => _resource = value;
            }
            public EResourceType ResourceType { get; }

            public TBXMDItem(string moduleName, string resource, EResourceType pathType)
            {
                Name = moduleName;
                ResourceType = pathType;
                Resource = resource;
                
                if (ResourceType == EResourceType.Internal) return;

                if (!File.Exists(Resource)) throw new FileNotFoundException($"Selected TBXMD file could not be found: {resource}", resource);
            }
            
            public void Update(string moduleName, string resource)
            {
                Name = moduleName;
                Resource = resource;
            }

            public override string ToString() => Text;

            public int CompareTo(TBXMDItem? other)
            {
                return Name.CompareTo(other?.Name);
            }
        }

        private bool _canBegin = false;
        private IFn? ClojureLoad
        {
            get
            {
                try
                {
                    return Clojure.var("clojure.core", "load-string");
                }
                catch (Exception ex) { throw ex; }
            }
        }
        private IFn? GeneratorMain { get; set; }
        private delegate bool IsValidDelegate();
        private List<IsValidDelegate> ValidationFunctions { get; }

        private List<string> ValidationFailureMessageBuffer { get; set; } = new List<string>();
        private Task? CycleMessages { get; set; }
        private CancellationTokenSource? CancellationTokenSource { get; set; }
        private CancellationToken? CancellationToken { get; set; }

        public TBXDialectSchemaGeneratorForm()
        {
            InitializeComponent();

            Task.Run(() =>
            {
                //string tbxDialectSchemaGenDir = Path.Join("clojure", "tbx_dialect_schema_generator");
                //string mainCljrTemp = Path.Join(
                //    tbxDialectSchemaGenDir,
                //    "main"
                //);
                //if (!Directory.Exists(tbxDialectSchemaGenDir)) Directory.CreateDirectory(tbxDialectSchemaGenDir);
                //File.WriteAllBytes($"{mainCljrTemp}.cljr", ClojureSrc.main);

                //ClojureLoad?.invoke($"/{mainCljrTemp}");
                ClojureLoad?.invoke(ClojureSrc.main);
                GeneratorMain = Clojure.var("tbx-dialect-schema-generator.main", "-main");

                _canBegin = true;
                Invoke(ValidateForm);
            });

            toolStripProgressBar.Style = ProgressBarStyle.Marquee;
            toolStripStatusLabel.Text = "Loading dependencies...";
            
            toolStripProgressBar.Style = ProgressBarStyle.Continuous;

            checkedListBox.Items.AddRange(new TBXMDItem[]
            {
                new TBXMDItem("Min", "Min", TBXMDItem.EResourceType.Internal),
                new TBXMDItem("Basic", "Basic", TBXMDItem.EResourceType.Internal)
            });

            ValidationFunctions = new List<IsValidDelegate>()
            {
                ValidateDialectName,
                ValidateModuleSelection,
                () => _canBegin
            };

            ValidateForm();
        }

        private void ValidateForm()
        {
            ValidationFailureMessageBuffer.Clear();
            toolStripStatusLabel.Text = "Validating...";
            toolStripStatusLabel.BackColor = Color.LightYellow;
            toolStripProgressBar.Style = ProgressBarStyle.Marquee;
            ToggleFormStatus(!ValidationFunctions.Select(isValid => !isValid()).ToList().Any(r => r));
        }

        private bool IsValidateDialectName(string dialectName) => Regex.IsMatch(dialectName, @"^TBX-\w+$");

        private bool ValidateDialectName()
        {
            if (string.IsNullOrWhiteSpace(dialectTextBox.Text))
            {
                ValidationFailureMessageBuffer.Add("TBX Dialect cannot be empty.");
                return false;
            }
            else if (!IsValidateDialectName(dialectTextBox.Text))
            {
                dialectTextBox.BackColor = Color.PaleVioletRed;
                ValidationFailureMessageBuffer.Add("TBX Dialect is not correctly formatted.");
                return false;
            } 
            else
            {
                dialectTextBox.BackColor = Color.White;
                return true;
            }
        }

        private bool ValidateModuleSelection() 
        {
            List<TBXMDItem> items = new List<TBXMDItem>(checkedListBox.CheckedItems.OfType<TBXMDItem>());
            if (items.DistinctBy(item => item.Name, StringComparer.OrdinalIgnoreCase).Count() < items.Count())
            {
                ValidationFailureMessageBuffer.Add("Selected modules must be distinct.");
                return false;
            }

            return true;
        }

        private async void FlushMessageBuffer(List<string> buffer)
        {
            if (CancellationTokenSource?.IsCancellationRequested == false)
            {
                CancellationTokenSource.Cancel();
            }

            
            if (CycleMessages == null || CancellationTokenSource?.IsCancellationRequested == true)
            {
                await Task.Run(() =>
                {
                    CycleMessages?.Wait();

                    while (true)
                    {
                        try
                        {
                            Invoke((MethodInvoker)(() =>
                            {
                                toolStripProgressBar.Style = ProgressBarStyle.Continuous;
                            }));
                            break;
                        }
                        catch (InvalidOperationException)
                        { }
                    }

                    CancellationTokenSource = new CancellationTokenSource();
                    CancellationToken = CancellationTokenSource.Token;
                    CycleMessages = Task.Run(() =>
                    {
                        while (CancellationToken?.IsCancellationRequested != true)
                        {
                            foreach (string m in buffer.ToArray())
                            {
                                if (CancellationToken?.IsCancellationRequested == true) break;
                                Thread.Sleep(2000);
                                Invoke((MethodInvoker)(() => toolStripStatusLabel.Text = m));
                            };
                        }

                        return;
                    }, CancellationTokenSource.Token);
                });
                
            }
        }

        private async void ToggleFormStatus(bool isValid, string? statusMessage = null)
        {
            if (isValid)
            {
                ValidationFailureMessageBuffer.Clear();  
                CancellationTokenSource?.Cancel();
                await Task.Run(() =>
                {
                    CycleMessages?.Wait();
                    Invoke((MethodInvoker)(()=>{ 
                        toolStripStatusLabel.Text = "Ready to generate!";
                        toolStripStatusLabel.BackColor = Color.LightSeaGreen;
                        dialectTextBox.BackColor = Color.White;
                        generateButton.Enabled = true;
                        toolStripProgressBar.Style = ProgressBarStyle.Continuous;
                    }));
                });
            } 
            else
            {
                toolStripStatusLabel.Text = statusMessage ?? "Not ready to generate...";
                if (ValidationFailureMessageBuffer.Any()) FlushMessageBuffer(ValidationFailureMessageBuffer); 
                toolStripStatusLabel.BackColor = Color.PaleVioletRed;
                generateButton.Enabled = false;
            }
        }

        private void dialectTextBox_TextChanged(object sender, EventArgs e)
        {
            if (ValidateDialectName())
            {
                dialectConfirmationButton.Enabled = true;
            }
            else
            {
                dialectConfirmationButton.Enabled = false;
            }
        }

        private void dialectConfirmationButton_Click(object sender, EventArgs e)
        {
            if (dialectConfirmationButton.Text.Equals("Modify")) 
            {
                dialectTextBox.Enabled = true;
                dialectConfirmationButton.Enabled = (dialectTextBox.Text.Any() && ValidateDialectName());
                dialectConfirmationButton.Text = "Confirm";
                return;
            }

            if (ValidateDialectName())
            {
                dialectTextBox.Enabled = false;
                dialectConfirmationButton.Text = "Modify";
                ValidateForm();
            }
        }

        private void checkedListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ValidateForm();

            if ((checkedListBox.SelectedItem as TBXMDItem)?.ResourceType == TBXMDItem.EResourceType.Internal)
            {
                editButton.Enabled = false;
                removeButton.Enabled = false;
            } else
            {
                editButton.Enabled = true;
                removeButton.Enabled = true;
            }
        }

        private void generateButton_Click(object sender, EventArgs e)
        {
            generateButton.Enabled = false;
            toolStripProgressBar.Style = ProgressBarStyle.Marquee;
            toolStripStatusLabel.BackColor = Color.White;
            toolStripStatusLabel.Text = "Generating...";
            Task.Run(() =>
            {
                Invoke((MethodInvoker)(() =>
                {
                    List<TBXMDItem> checkedItems = new List<TBXMDItem>(checkedListBox.CheckedItems.OfType<TBXMDItem>());
                    List<string> args = new List<string>() { "output.sch", dialectTextBox.Text };
                    args.AddRange(checkedItems.Select(item => item.Resource ?? ""));

                    try
                    {
                        var ret = GeneratorMain?.invoke(args);
                    } catch (Exception ex)
                    {
                        toolStripProgressBar.Style = ProgressBarStyle.Continuous;
                        toolStripStatusLabel.BackColor = Color.IndianRed;
                        toolStripStatusLabel.Text = $"Generation failed: {ex.Message}";
                        generateButton.Enabled = true;
                        return;
                    }

                    toolStripProgressBar.Style = ProgressBarStyle.Continuous;
                    toolStripStatusLabel.BackColor = Color.White;
                    toolStripStatusLabel.Text = "Complete.";
                    generateButton.Enabled = true;
                }));
            });
        }

        private void ModuleItemModifierFormClosed(string moduleName, string modulePath)
        {
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            using (ModuleItemModifierForm moduleItemModifierForm = new ModuleItemModifierForm())
            {
                if (moduleItemModifierForm.ShowDialog() == DialogResult.OK)
                {
                    string moduleName = moduleItemModifierForm.ModuleName;
                    string modulePath = moduleItemModifierForm.ModulePath;
                    if (string.IsNullOrWhiteSpace(moduleName) || string.IsNullOrWhiteSpace(modulePath)) return;

                    checkedListBox.Items.Add(new TBXMDItem(moduleName, modulePath, TBXMDItem.EResourceType.External));
                }
            }
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            TBXMDItem? selectedItem = checkedListBox.SelectedItem as TBXMDItem;
            if (selectedItem == null) return;

            using (ModuleItemModifierForm moduleItemModifierForm = new ModuleItemModifierForm(selectedItem.Name, selectedItem.Resource))
            {
                if (moduleItemModifierForm.ShowDialog() == DialogResult.OK)
                {
                    string moduleName = moduleItemModifierForm.ModuleName;
                    string modulePath = moduleItemModifierForm.ModulePath;
                    if (string.IsNullOrWhiteSpace(moduleName) || string.IsNullOrWhiteSpace(modulePath)) return;

                    selectedItem.Update(moduleName, modulePath);
                    checkedListBox.Refresh();
                }
            }
        }
    }
}