namespace TBXDialectSchemaGenerator
{
    partial class TBXDialectSchemaGeneratorForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newSchemaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateInternalTBXMDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dialectConfirmationButton = new System.Windows.Forms.Button();
            this.generateButton = new System.Windows.Forms.Button();
            this.removeButton = new System.Windows.Forms.Button();
            this.editButton = new System.Windows.Forms.Button();
            this.addButton = new System.Windows.Forms.Button();
            this.checkedListBox = new System.Windows.Forms.CheckedListBox();
            this.tbxmdLabel = new System.Windows.Forms.Label();
            this.dialectTextBox = new System.Windows.Forms.TextBox();
            this.dialectNameLabel = new System.Windows.Forms.Label();
            this.statusStripRight = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.statusStripLeft = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.statusStripRight.SuspendLayout();
            this.statusStripLeft.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(400, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newSchemaToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Enabled = false;
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newSchemaToolStripMenuItem
            // 
            this.newSchemaToolStripMenuItem.Name = "newSchemaToolStripMenuItem";
            this.newSchemaToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.newSchemaToolStripMenuItem.Text = "New Schema";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.updateInternalTBXMDToolStripMenuItem,
            this.toolStripMenuItem2});
            this.settingsToolStripMenuItem.Enabled = false;
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // updateInternalTBXMDToolStripMenuItem
            // 
            this.updateInternalTBXMDToolStripMenuItem.Name = "updateInternalTBXMDToolStripMenuItem";
            this.updateInternalTBXMDToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.updateInternalTBXMDToolStripMenuItem.Text = "Update Internal TBXMD files";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(221, 22);
            this.toolStripMenuItem2.Text = "Configure Web Locations";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Enabled = false;
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.dialectConfirmationButton);
            this.panel1.Controls.Add(this.generateButton);
            this.panel1.Controls.Add(this.removeButton);
            this.panel1.Controls.Add(this.editButton);
            this.panel1.Controls.Add(this.addButton);
            this.panel1.Controls.Add(this.checkedListBox);
            this.panel1.Controls.Add(this.tbxmdLabel);
            this.panel1.Controls.Add(this.dialectTextBox);
            this.panel1.Controls.Add(this.dialectNameLabel);
            this.panel1.Location = new System.Drawing.Point(0, 27);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(400, 297);
            this.panel1.TabIndex = 2;
            // 
            // dialectConfirmationButton
            // 
            this.dialectConfirmationButton.Location = new System.Drawing.Point(298, 29);
            this.dialectConfirmationButton.Name = "dialectConfirmationButton";
            this.dialectConfirmationButton.Size = new System.Drawing.Size(90, 23);
            this.dialectConfirmationButton.TabIndex = 7;
            this.dialectConfirmationButton.Text = "Modify";
            this.dialectConfirmationButton.UseVisualStyleBackColor = true;
            this.dialectConfirmationButton.Click += new System.EventHandler(this.dialectConfirmationButton_Click);
            // 
            // generateButton
            // 
            this.generateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.generateButton.Location = new System.Drawing.Point(298, 261);
            this.generateButton.Name = "generateButton";
            this.generateButton.Size = new System.Drawing.Size(90, 23);
            this.generateButton.TabIndex = 1;
            this.generateButton.Text = "Generate";
            this.generateButton.UseVisualStyleBackColor = true;
            this.generateButton.Click += new System.EventHandler(this.generateButton_Click);
            // 
            // removeButton
            // 
            this.removeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.removeButton.Enabled = false;
            this.removeButton.Location = new System.Drawing.Point(298, 152);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(90, 23);
            this.removeButton.TabIndex = 6;
            this.removeButton.Text = "Remove";
            this.removeButton.UseVisualStyleBackColor = true;
            // 
            // editButton
            // 
            this.editButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.editButton.Enabled = false;
            this.editButton.Location = new System.Drawing.Point(298, 123);
            this.editButton.Name = "editButton";
            this.editButton.Size = new System.Drawing.Size(90, 23);
            this.editButton.TabIndex = 6;
            this.editButton.Text = "Edit";
            this.editButton.UseVisualStyleBackColor = true;
            this.editButton.Click += new System.EventHandler(this.editButton_Click);
            // 
            // addButton
            // 
            this.addButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.addButton.Location = new System.Drawing.Point(298, 94);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(90, 23);
            this.addButton.TabIndex = 6;
            this.addButton.Text = "Add";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // checkedListBox
            // 
            this.checkedListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkedListBox.CheckOnClick = true;
            this.checkedListBox.FormattingEnabled = true;
            this.checkedListBox.Location = new System.Drawing.Point(12, 94);
            this.checkedListBox.Name = "checkedListBox";
            this.checkedListBox.ScrollAlwaysVisible = true;
            this.checkedListBox.Size = new System.Drawing.Size(280, 148);
            this.checkedListBox.TabIndex = 5;
            this.checkedListBox.SelectedIndexChanged += new System.EventHandler(this.checkedListBox_SelectedIndexChanged);
            // 
            // tbxmdLabel
            // 
            this.tbxmdLabel.AutoSize = true;
            this.tbxmdLabel.Location = new System.Drawing.Point(12, 68);
            this.tbxmdLabel.Name = "tbxmdLabel";
            this.tbxmdLabel.Size = new System.Drawing.Size(368, 15);
            this.tbxmdLabel.TabIndex = 3;
            this.tbxmdLabel.Text = "Select module .tbxmd files from which to generate a dialect schema:";
            // 
            // dialectTextBox
            // 
            this.dialectTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dialectTextBox.Enabled = false;
            this.dialectTextBox.Location = new System.Drawing.Point(12, 29);
            this.dialectTextBox.Name = "dialectTextBox";
            this.dialectTextBox.PlaceholderText = "TBX-[Unique Name] (TBX-Min, TBX-Basic, etc.)";
            this.dialectTextBox.Size = new System.Drawing.Size(280, 23);
            this.dialectTextBox.TabIndex = 2;
            this.dialectTextBox.TextChanged += new System.EventHandler(this.dialectTextBox_TextChanged);
            // 
            // dialectNameLabel
            // 
            this.dialectNameLabel.AutoSize = true;
            this.dialectNameLabel.Location = new System.Drawing.Point(12, 11);
            this.dialectNameLabel.Name = "dialectNameLabel";
            this.dialectNameLabel.Size = new System.Drawing.Size(78, 15);
            this.dialectNameLabel.TabIndex = 0;
            this.dialectNameLabel.Text = "Dialect Name";
            // 
            // statusStripRight
            // 
            this.statusStripRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.statusStripRight.Dock = System.Windows.Forms.DockStyle.None;
            this.statusStripRight.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar});
            this.statusStripRight.Location = new System.Drawing.Point(281, 327);
            this.statusStripRight.Name = "statusStripRight";
            this.statusStripRight.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.statusStripRight.Size = new System.Drawing.Size(119, 22);
            this.statusStripRight.SizingGrip = false;
            this.statusStripRight.Stretch = false;
            this.statusStripRight.TabIndex = 3;
            this.statusStripRight.Text = "statusStrip";
            // 
            // toolStripProgressBar
            // 
            this.toolStripProgressBar.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripProgressBar.AutoSize = false;
            this.toolStripProgressBar.Name = "toolStripProgressBar";
            this.toolStripProgressBar.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripProgressBar.Size = new System.Drawing.Size(100, 16);
            // 
            // statusStripLeft
            // 
            this.statusStripLeft.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStripLeft.Location = new System.Drawing.Point(0, 327);
            this.statusStripLeft.Name = "statusStripLeft";
            this.statusStripLeft.Size = new System.Drawing.Size(400, 22);
            this.statusStripLeft.SizingGrip = false;
            this.statusStripLeft.TabIndex = 4;
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabel.Text = "toolStripStatusLabel1";
            // 
            // TBXDialectSchemaGeneratorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 349);
            this.Controls.Add(this.statusStripRight);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.statusStripLeft);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximumSize = new System.Drawing.Size(999999, 388);
            this.MinimumSize = new System.Drawing.Size(416, 388);
            this.Name = "TBXDialectSchemaGeneratorForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "TBX Dialect Schematron Generator";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.statusStripRight.ResumeLayout(false);
            this.statusStripRight.PerformLayout();
            this.statusStripLeft.ResumeLayout(false);
            this.statusStripLeft.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem newSchemaToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private Panel panel1;
        private Button removeButton;
        private Button editButton;
        private Button addButton;
        private CheckedListBox checkedListBox;
        private Label tbxmdLabel;
        private TextBox dialectTextBox;
        private Label dialectNameLabel;
        private StatusStrip statusStripRight;
        private ToolStripProgressBar toolStripProgressBar;
        private ToolStripMenuItem settingsToolStripMenuItem;
        private ToolStripMenuItem updateInternalTBXMDToolStripMenuItem;
        private ToolStripMenuItem toolStripMenuItem2;
        private StatusStrip statusStripLeft;
        private ToolStripStatusLabel toolStripStatusLabel;
        private Button generateButton;
        private Button dialectConfirmationButton;
    }
}