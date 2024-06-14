namespace dosbox_staging_vct
{
    partial class FormOptions
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
            GroupBoxPaths = new GroupBox();
            ButtonMakeInstallationPortable = new Button();
            LabelInstallationPortable = new Label();
            ButtonSetUserConfFolderPath = new Button();
            TextBoxUserConfFolderPath = new TextBox();
            LabelUserConfFolderPath = new Label();
            TextBoxGlobalConfFilePath = new TextBox();
            LabelGlobalConfFilePath = new Label();
            ButtonSetDosBoxStagingExeFilePath = new Button();
            TextBoxDosBoxStagingExeFilePath = new TextBox();
            LabelDosBoxStagingExeFilePath = new Label();
            GroupBoxCustomization = new GroupBox();
            ComboBoxLaguage = new ComboBox();
            LabelLaguage = new Label();
            ButtonSetJsonOptionsFile = new Button();
            TextBoxJsonOptionsFile = new TextBox();
            LabelJsonOptionsFile = new Label();
            GroupBoxParameters = new GroupBox();
            TextBoxParameters = new TextBox();
            LabelParameters = new Label();
            GroupBoxPaths.SuspendLayout();
            GroupBoxCustomization.SuspendLayout();
            GroupBoxParameters.SuspendLayout();
            SuspendLayout();
            // 
            // GroupBoxPaths
            // 
            GroupBoxPaths.Controls.Add(ButtonMakeInstallationPortable);
            GroupBoxPaths.Controls.Add(LabelInstallationPortable);
            GroupBoxPaths.Controls.Add(ButtonSetUserConfFolderPath);
            GroupBoxPaths.Controls.Add(TextBoxUserConfFolderPath);
            GroupBoxPaths.Controls.Add(LabelUserConfFolderPath);
            GroupBoxPaths.Controls.Add(TextBoxGlobalConfFilePath);
            GroupBoxPaths.Controls.Add(LabelGlobalConfFilePath);
            GroupBoxPaths.Controls.Add(ButtonSetDosBoxStagingExeFilePath);
            GroupBoxPaths.Controls.Add(TextBoxDosBoxStagingExeFilePath);
            GroupBoxPaths.Controls.Add(LabelDosBoxStagingExeFilePath);
            GroupBoxPaths.Location = new Point(12, 12);
            GroupBoxPaths.Name = "GroupBoxPaths";
            GroupBoxPaths.Padding = new Padding(10);
            GroupBoxPaths.Size = new Size(560, 198);
            GroupBoxPaths.TabIndex = 0;
            GroupBoxPaths.TabStop = false;
            GroupBoxPaths.Text = "Paths";
            // 
            // ButtonMakeInstallationPortable
            // 
            ButtonMakeInstallationPortable.Enabled = false;
            ButtonMakeInstallationPortable.ImeMode = ImeMode.NoControl;
            ButtonMakeInstallationPortable.Location = new Point(433, 99);
            ButtonMakeInstallationPortable.Margin = new Padding(3, 2, 3, 2);
            ButtonMakeInstallationPortable.Name = "ButtonMakeInstallationPortable";
            ButtonMakeInstallationPortable.Size = new Size(114, 22);
            ButtonMakeInstallationPortable.TabIndex = 6;
            ButtonMakeInstallationPortable.Text = "Make portable";
            ButtonMakeInstallationPortable.UseVisualStyleBackColor = true;
            ButtonMakeInstallationPortable.Click += ButtonMakeInstallationPortable_Click;
            // 
            // LabelInstallationPortable
            // 
            LabelInstallationPortable.ForeColor = SystemColors.ControlText;
            LabelInstallationPortable.ImeMode = ImeMode.NoControl;
            LabelInstallationPortable.Location = new Point(213, 81);
            LabelInstallationPortable.Name = "LabelInstallationPortable";
            LabelInstallationPortable.Size = new Size(208, 15);
            LabelInstallationPortable.TabIndex = 4;
            LabelInstallationPortable.Text = "The installation is not portable";
            LabelInstallationPortable.TextAlign = ContentAlignment.TopRight;
            // 
            // ButtonSetUserConfFolderPath
            // 
            ButtonSetUserConfFolderPath.ImeMode = ImeMode.NoControl;
            ButtonSetUserConfFolderPath.Location = new Point(433, 153);
            ButtonSetUserConfFolderPath.Name = "ButtonSetUserConfFolderPath";
            ButtonSetUserConfFolderPath.Size = new Size(24, 23);
            ButtonSetUserConfFolderPath.TabIndex = 9;
            ButtonSetUserConfFolderPath.Text = "...";
            ButtonSetUserConfFolderPath.UseVisualStyleBackColor = true;
            ButtonSetUserConfFolderPath.Click += ButtonSetUserConfFolderPath_Click;
            // 
            // TextBoxUserConfFolderPath
            // 
            TextBoxUserConfFolderPath.Location = new Point(13, 153);
            TextBoxUserConfFolderPath.Margin = new Padding(5, 2, 5, 15);
            TextBoxUserConfFolderPath.Name = "TextBoxUserConfFolderPath";
            TextBoxUserConfFolderPath.ReadOnly = true;
            TextBoxUserConfFolderPath.Size = new Size(408, 23);
            TextBoxUserConfFolderPath.TabIndex = 8;
            // 
            // LabelUserConfFolderPath
            // 
            LabelUserConfFolderPath.AutoSize = true;
            LabelUserConfFolderPath.ImeMode = ImeMode.NoControl;
            LabelUserConfFolderPath.Location = new Point(13, 136);
            LabelUserConfFolderPath.Name = "LabelUserConfFolderPath";
            LabelUserConfFolderPath.Size = new Size(123, 15);
            LabelUserConfFolderPath.TabIndex = 7;
            LabelUserConfFolderPath.Text = "User confs folder path";
            // 
            // TextBoxGlobalConfFilePath
            // 
            TextBoxGlobalConfFilePath.Location = new Point(13, 98);
            TextBoxGlobalConfFilePath.Margin = new Padding(5, 2, 5, 15);
            TextBoxGlobalConfFilePath.Name = "TextBoxGlobalConfFilePath";
            TextBoxGlobalConfFilePath.ReadOnly = true;
            TextBoxGlobalConfFilePath.Size = new Size(408, 23);
            TextBoxGlobalConfFilePath.TabIndex = 5;
            // 
            // LabelGlobalConfFilePath
            // 
            LabelGlobalConfFilePath.AutoSize = true;
            LabelGlobalConfFilePath.ImeMode = ImeMode.NoControl;
            LabelGlobalConfFilePath.Location = new Point(13, 81);
            LabelGlobalConfFilePath.Name = "LabelGlobalConfFilePath";
            LabelGlobalConfFilePath.Size = new Size(114, 15);
            LabelGlobalConfFilePath.TabIndex = 3;
            LabelGlobalConfFilePath.Text = "Global conf file path";
            // 
            // ButtonSetDosBoxStagingExeFilePath
            // 
            ButtonSetDosBoxStagingExeFilePath.ImeMode = ImeMode.NoControl;
            ButtonSetDosBoxStagingExeFilePath.Location = new Point(433, 43);
            ButtonSetDosBoxStagingExeFilePath.Name = "ButtonSetDosBoxStagingExeFilePath";
            ButtonSetDosBoxStagingExeFilePath.Size = new Size(24, 23);
            ButtonSetDosBoxStagingExeFilePath.TabIndex = 0;
            ButtonSetDosBoxStagingExeFilePath.Text = "...";
            ButtonSetDosBoxStagingExeFilePath.UseVisualStyleBackColor = true;
            ButtonSetDosBoxStagingExeFilePath.Click += ButtonSetDosBoxStagingExeFilePath_Click;
            // 
            // TextBoxDosBoxStagingExeFilePath
            // 
            TextBoxDosBoxStagingExeFilePath.Location = new Point(13, 43);
            TextBoxDosBoxStagingExeFilePath.Margin = new Padding(5, 2, 5, 15);
            TextBoxDosBoxStagingExeFilePath.Name = "TextBoxDosBoxStagingExeFilePath";
            TextBoxDosBoxStagingExeFilePath.ReadOnly = true;
            TextBoxDosBoxStagingExeFilePath.Size = new Size(408, 23);
            TextBoxDosBoxStagingExeFilePath.TabIndex = 1;
            // 
            // LabelDosBoxStagingExeFilePath
            // 
            LabelDosBoxStagingExeFilePath.AutoSize = true;
            LabelDosBoxStagingExeFilePath.ImeMode = ImeMode.NoControl;
            LabelDosBoxStagingExeFilePath.Location = new Point(13, 26);
            LabelDosBoxStagingExeFilePath.Name = "LabelDosBoxStagingExeFilePath";
            LabelDosBoxStagingExeFilePath.Size = new Size(162, 15);
            LabelDosBoxStagingExeFilePath.TabIndex = 0;
            LabelDosBoxStagingExeFilePath.Text = "DOSBox-Staging exe file path";
            // 
            // GroupBoxCustomization
            // 
            GroupBoxCustomization.Controls.Add(ComboBoxLaguage);
            GroupBoxCustomization.Controls.Add(LabelLaguage);
            GroupBoxCustomization.Controls.Add(ButtonSetJsonOptionsFile);
            GroupBoxCustomization.Controls.Add(TextBoxJsonOptionsFile);
            GroupBoxCustomization.Controls.Add(LabelJsonOptionsFile);
            GroupBoxCustomization.Location = new Point(12, 323);
            GroupBoxCustomization.Name = "GroupBoxCustomization";
            GroupBoxCustomization.Padding = new Padding(10);
            GroupBoxCustomization.Size = new Size(560, 93);
            GroupBoxCustomization.TabIndex = 2;
            GroupBoxCustomization.TabStop = false;
            GroupBoxCustomization.Text = "Constomization";
            // 
            // ComboBoxLaguage
            // 
            ComboBoxLaguage.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBoxLaguage.FormattingEnabled = true;
            ComboBoxLaguage.Location = new Point(367, 43);
            ComboBoxLaguage.Margin = new Padding(5, 2, 15, 15);
            ComboBoxLaguage.Name = "ComboBoxLaguage";
            ComboBoxLaguage.Size = new Size(180, 23);
            ComboBoxLaguage.TabIndex = 4;
            // 
            // LabelLaguage
            // 
            LabelLaguage.AutoSize = true;
            LabelLaguage.ImeMode = ImeMode.NoControl;
            LabelLaguage.Location = new Point(367, 26);
            LabelLaguage.Name = "LabelLaguage";
            LabelLaguage.Size = new Size(59, 15);
            LabelLaguage.TabIndex = 3;
            LabelLaguage.Text = "Language";
            // 
            // ButtonSetJsonOptionsFile
            // 
            ButtonSetJsonOptionsFile.ImeMode = ImeMode.NoControl;
            ButtonSetJsonOptionsFile.Location = new Point(201, 43);
            ButtonSetJsonOptionsFile.Name = "ButtonSetJsonOptionsFile";
            ButtonSetJsonOptionsFile.Size = new Size(24, 23);
            ButtonSetJsonOptionsFile.TabIndex = 2;
            ButtonSetJsonOptionsFile.Text = "...";
            ButtonSetJsonOptionsFile.UseVisualStyleBackColor = true;
            // 
            // TextBoxJsonOptionsFile
            // 
            TextBoxJsonOptionsFile.Location = new Point(13, 43);
            TextBoxJsonOptionsFile.Margin = new Padding(5, 2, 5, 15);
            TextBoxJsonOptionsFile.Name = "TextBoxJsonOptionsFile";
            TextBoxJsonOptionsFile.ReadOnly = true;
            TextBoxJsonOptionsFile.Size = new Size(180, 23);
            TextBoxJsonOptionsFile.TabIndex = 1;
            // 
            // LabelJsonOptionsFile
            // 
            LabelJsonOptionsFile.AutoSize = true;
            LabelJsonOptionsFile.ImeMode = ImeMode.NoControl;
            LabelJsonOptionsFile.Location = new Point(13, 26);
            LabelJsonOptionsFile.Name = "LabelJsonOptionsFile";
            LabelJsonOptionsFile.Size = new Size(97, 15);
            LabelJsonOptionsFile.TabIndex = 0;
            LabelJsonOptionsFile.Text = "JSON options file";
            // 
            // GroupBoxParameters
            // 
            GroupBoxParameters.Controls.Add(TextBoxParameters);
            GroupBoxParameters.Controls.Add(LabelParameters);
            GroupBoxParameters.Location = new Point(12, 216);
            GroupBoxParameters.Name = "GroupBoxParameters";
            GroupBoxParameters.Padding = new Padding(10);
            GroupBoxParameters.Size = new Size(560, 101);
            GroupBoxParameters.TabIndex = 1;
            GroupBoxParameters.TabStop = false;
            GroupBoxParameters.Text = "Parameters";
            // 
            // TextBoxParameters
            // 
            TextBoxParameters.Location = new Point(13, 51);
            TextBoxParameters.Name = "TextBoxParameters";
            TextBoxParameters.Size = new Size(534, 23);
            TextBoxParameters.TabIndex = 1;
            // 
            // LabelParameters
            // 
            LabelParameters.AutoSize = true;
            LabelParameters.ImeMode = ImeMode.NoControl;
            LabelParameters.Location = new Point(13, 33);
            LabelParameters.Name = "LabelParameters";
            LabelParameters.Size = new Size(274, 15);
            LabelParameters.TabIndex = 0;
            LabelParameters.Text = "The parameters below will be passed first at launch";
            // 
            // FormOptions
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(584, 428);
            Controls.Add(GroupBoxParameters);
            Controls.Add(GroupBoxCustomization);
            Controls.Add(GroupBoxPaths);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormOptions";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Options";
            FormClosing += FormOptions_FormClosing;
            Load += FormOptions_Load;
            GroupBoxPaths.ResumeLayout(false);
            GroupBoxPaths.PerformLayout();
            GroupBoxCustomization.ResumeLayout(false);
            GroupBoxCustomization.PerformLayout();
            GroupBoxParameters.ResumeLayout(false);
            GroupBoxParameters.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox GroupBoxPaths;
        private Button ButtonSetDosBoxStagingExeFilePath;
        private TextBox TextBoxDosBoxStagingExeFilePath;
        private Label LabelDosBoxStagingExeFilePath;
        private TextBox TextBoxGlobalConfFilePath;
        private Label LabelGlobalConfFilePath;
        private Button ButtonSetUserConfFolderPath;
        private TextBox TextBoxUserConfFolderPath;
        private Label LabelUserConfFolderPath;
        private Label LabelInstallationPortable;
        private Button ButtonMakeInstallationPortable;
        private GroupBox GroupBoxCustomization;
        private Button ButtonSetJsonOptionsFile;
        private TextBox TextBoxJsonOptionsFile;
        private Label LabelJsonOptionsFile;
        private ComboBox ComboBoxLaguage;
        private Label LabelLaguage;
        private GroupBox GroupBoxParameters;
        private TextBox TextBoxParameters;
        private Label LabelParameters;
    }
}