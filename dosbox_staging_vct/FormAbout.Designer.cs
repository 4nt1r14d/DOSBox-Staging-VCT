namespace dosbox_staging_vct
{
    partial class FormAbout
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAbout));
            LabelVersion = new Label();
            PictureBoxLogo = new PictureBox();
            ButtonClose = new Button();
            pictureBox1 = new PictureBox();
            LabelInfo = new Label();
            LinkLabelDosboxStaging = new LinkLabel();
            label1 = new Label();
            LinkLabelGitHubPage = new LinkLabel();
            ((System.ComponentModel.ISupportInitialize)PictureBoxLogo).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // LabelVersion
            // 
            LabelVersion.AutoSize = true;
            LabelVersion.Location = new Point(672, 12);
            LabelVersion.Name = "LabelVersion";
            LabelVersion.Size = new Size(45, 15);
            LabelVersion.TabIndex = 4;
            LabelVersion.Text = "Version";
            // 
            // PictureBoxLogo
            // 
            PictureBoxLogo.Image = Resources.Icons.logo;
            PictureBoxLogo.Location = new Point(12, 12);
            PictureBoxLogo.Name = "PictureBoxLogo";
            PictureBoxLogo.Size = new Size(254, 311);
            PictureBoxLogo.SizeMode = PictureBoxSizeMode.AutoSize;
            PictureBoxLogo.TabIndex = 2;
            PictureBoxLogo.TabStop = false;
            // 
            // ButtonClose
            // 
            ButtonClose.Location = new Point(642, 315);
            ButtonClose.Name = "ButtonClose";
            ButtonClose.Size = new Size(75, 23);
            ButtonClose.TabIndex = 0;
            ButtonClose.Text = "Close";
            ButtonClose.UseVisualStyleBackColor = true;
            ButtonClose.Click += ButtonClose_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Resources.Icons.dosbox_logor;
            pictureBox1.Location = new Point(283, 248);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(66, 75);
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox1.TabIndex = 4;
            pictureBox1.TabStop = false;
            // 
            // LabelInfo
            // 
            LabelInfo.Location = new Point(283, 12);
            LabelInfo.Name = "LabelInfo";
            LabelInfo.Size = new Size(434, 205);
            LabelInfo.TabIndex = 3;
            LabelInfo.Text = resources.GetString("LabelInfo.Text");
            // 
            // LinkLabelDosboxStaging
            // 
            LinkLabelDosboxStaging.ActiveLinkColor = SystemColors.ControlDarkDark;
            LinkLabelDosboxStaging.AutoSize = true;
            LinkLabelDosboxStaging.Location = new Point(394, 308);
            LinkLabelDosboxStaging.Name = "LinkLabelDosboxStaging";
            LinkLabelDosboxStaging.Size = new Size(186, 15);
            LinkLabelDosboxStaging.TabIndex = 1;
            LinkLabelDosboxStaging.TabStop = true;
            LinkLabelDosboxStaging.Text = "https://www.dosbox-staging.org/";
            LinkLabelDosboxStaging.LinkClicked += LinkLabelDosboxStaging_LinkClicked;
            // 
            // label1
            // 
            label1.Location = new Point(367, 248);
            label1.Name = "label1";
            label1.Size = new Size(350, 52);
            label1.TabIndex = 2;
            label1.Text = "In order to use this application, it is necessary to previously install DOSBox Staging. Please visit the official website to learn more about this fantastic project and download the latest version.";
            // 
            // LinkLabelGitHubPage
            // 
            LinkLabelGitHubPage.ActiveLinkColor = SystemColors.ControlDarkDark;
            LinkLabelGitHubPage.AutoSize = true;
            LinkLabelGitHubPage.Location = new Point(351, 217);
            LinkLabelGitHubPage.Name = "LinkLabelGitHubPage";
            LinkLabelGitHubPage.Size = new Size(284, 15);
            LinkLabelGitHubPage.TabIndex = 5;
            LinkLabelGitHubPage.TabStop = true;
            LinkLabelGitHubPage.Text = "https://github.com/4nt1r14d/DOSBox-Staging-VCT/";
            LinkLabelGitHubPage.LinkClicked += LinkLabelGitHubPage_LinkClicked;
            // 
            // FormAbout
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(729, 350);
            Controls.Add(LinkLabelGitHubPage);
            Controls.Add(label1);
            Controls.Add(LinkLabelDosboxStaging);
            Controls.Add(pictureBox1);
            Controls.Add(ButtonClose);
            Controls.Add(PictureBoxLogo);
            Controls.Add(LabelVersion);
            Controls.Add(LabelInfo);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormAbout";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "About";
            Load += FormAbout_Load;
            ((System.ComponentModel.ISupportInitialize)PictureBoxLogo).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label LabelVersion;
        private PictureBox PictureBoxLogo;
        private Button ButtonClose;
        private PictureBox pictureBox1;
        private Label LabelInfo;
        private LinkLabel LinkLabelDosboxStaging;
        private Label label1;
        private LinkLabel LinkLabelGitHubPage;
    }
}