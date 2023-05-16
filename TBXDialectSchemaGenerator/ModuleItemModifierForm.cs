using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TBXDialectSchemaGenerator
{
    public partial class ModuleItemModifierForm : Form
    {
        public string ModuleName { get => moduleNameTextBox.Text; set => moduleNameTextBox.Text = value; }
        public string ModulePath { get => modulePathTextBox.Text; set => modulePathTextBox.Text = value; }

        public ModuleItemModifierForm(string name = "", string path = "")
        {
            InitializeComponent();

            ModuleName = name;
            ModulePath = path;
        }

        private void modulePathTextBox_Leave(object sender, EventArgs e)
        {
            if (Path.Exists(modulePathTextBox.Text))
            {
                modulePathTextBox.BackColor = Color.White;
                confirmButton.Enabled = true;
                return;
            }

            modulePathTextBox.BackColor = Color.IndianRed;
            confirmButton.Enabled = false;
        }

        private void confirmButton_Validated(object sender, EventArgs e)
        {
            Close();
        }

        private void confirmButton_Click(object sender, EventArgs e)
        {
            if (!Path.Exists(ModulePath))
            {
                modulePathTextBox.BackColor = Color.IndianRed;
            }
            if (string.IsNullOrWhiteSpace(ModuleName)) 
            {
                moduleNameTextBox.BackColor = Color.IndianRed;
            } else DialogResult = DialogResult.OK;

        }

        private void moduleBrowseButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = Environment.SpecialFolder.UserProfile.ToString();
                openFileDialog.Filter = "TBXMD files (*.tbxmd)|*.tbxmd";
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    ModulePath = openFileDialog.FileName;
                    modulePathTextBox.BackColor = Color.White;
                    confirmButton.Enabled = true;
                }
            }
        }
    }
}
