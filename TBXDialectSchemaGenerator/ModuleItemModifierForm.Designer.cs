namespace TBXDialectSchemaGenerator
{
    partial class ModuleItemModifierForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.moduleNameTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.modulePathTextBox = new System.Windows.Forms.TextBox();
            this.moduleBrowseButton = new System.Windows.Forms.Button();
            this.confirmButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // moduleNameTextBox
            // 
            this.moduleNameTextBox.Location = new System.Drawing.Point(268, 10);
            this.moduleNameTextBox.Name = "moduleNameTextBox";
            this.moduleNameTextBox.PlaceholderText = "Min, Basic, etc.";
            this.moduleNameTextBox.Size = new System.Drawing.Size(100, 23);
            this.moduleNameTextBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(230, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Select Module Name (\"Min\", \"Basic\", etc.):";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(161, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Path to module\'s TBXMD file:";
            // 
            // modulePathTextBox
            // 
            this.modulePathTextBox.Location = new System.Drawing.Point(268, 43);
            this.modulePathTextBox.Name = "modulePathTextBox";
            this.modulePathTextBox.Size = new System.Drawing.Size(246, 23);
            this.modulePathTextBox.TabIndex = 3;
            this.modulePathTextBox.Leave += new System.EventHandler(this.modulePathTextBox_Leave);
            // 
            // moduleBrowseButton
            // 
            this.moduleBrowseButton.Location = new System.Drawing.Point(179, 43);
            this.moduleBrowseButton.Name = "moduleBrowseButton";
            this.moduleBrowseButton.Size = new System.Drawing.Size(75, 23);
            this.moduleBrowseButton.TabIndex = 4;
            this.moduleBrowseButton.Text = "Browse";
            this.moduleBrowseButton.UseVisualStyleBackColor = true;
            this.moduleBrowseButton.Click += new System.EventHandler(this.moduleBrowseButton_Click);
            // 
            // confirmButton
            // 
            this.confirmButton.Location = new System.Drawing.Point(197, 92);
            this.confirmButton.Name = "confirmButton";
            this.confirmButton.Size = new System.Drawing.Size(111, 23);
            this.confirmButton.TabIndex = 5;
            this.confirmButton.Text = "Confirm";
            this.confirmButton.UseVisualStyleBackColor = true;
            this.confirmButton.Click += new System.EventHandler(this.confirmButton_Click);
            this.confirmButton.Validated += new System.EventHandler(this.confirmButton_Validated);
            // 
            // ModuleItemModifierForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(528, 127);
            this.Controls.Add(this.confirmButton);
            this.Controls.Add(this.moduleBrowseButton);
            this.Controls.Add(this.modulePathTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.moduleNameTextBox);
            this.Name = "ModuleItemModifierForm";
            this.Text = "ModuleItemModifier";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox moduleNameTextBox;
        private Label label1;
        private Label label2;
        private TextBox modulePathTextBox;
        private Button moduleBrowseButton;
        private Button confirmButton;
    }
}