namespace dosbox_staging_vct
{
    partial class FormHelp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormHelp));
            TabControlHelp = new TabControl();
            TabPageManual = new TabPage();
            TextBoxManual = new TextBox();
            TabPageConfFile = new TabPage();
            panel1 = new Panel();
            RichTextBoxConfFile = new RichTextBox();
            ButtonClose = new Button();
            TabControlHelp.SuspendLayout();
            TabPageManual.SuspendLayout();
            TabPageConfFile.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // TabControlHelp
            // 
            TabControlHelp.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            TabControlHelp.Controls.Add(TabPageManual);
            TabControlHelp.Controls.Add(TabPageConfFile);
            TabControlHelp.Location = new Point(12, 12);
            TabControlHelp.Name = "TabControlHelp";
            TabControlHelp.SelectedIndex = 0;
            TabControlHelp.Size = new Size(776, 397);
            TabControlHelp.TabIndex = 0;
            // 
            // TabPageManual
            // 
            TabPageManual.Controls.Add(TextBoxManual);
            TabPageManual.Location = new Point(4, 24);
            TabPageManual.Name = "TabPageManual";
            TabPageManual.Padding = new Padding(3);
            TabPageManual.Size = new Size(768, 369);
            TabPageManual.TabIndex = 0;
            TabPageManual.Text = "DOSBox Staging Manual";
            TabPageManual.UseVisualStyleBackColor = true;
            // 
            // TextBoxManual
            // 
            TextBoxManual.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            TextBoxManual.BorderStyle = BorderStyle.FixedSingle;
            TextBoxManual.Font = new Font("Consolas", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TextBoxManual.Location = new Point(13, 13);
            TextBoxManual.Margin = new Padding(10);
            TextBoxManual.Multiline = true;
            TextBoxManual.Name = "TextBoxManual";
            TextBoxManual.ReadOnly = true;
            TextBoxManual.ScrollBars = ScrollBars.Vertical;
            TextBoxManual.Size = new Size(745, 343);
            TextBoxManual.TabIndex = 0;
            // 
            // TabPageConfFile
            // 
            TabPageConfFile.Controls.Add(panel1);
            TabPageConfFile.Location = new Point(4, 24);
            TabPageConfFile.Name = "TabPageConfFile";
            TabPageConfFile.Size = new Size(768, 369);
            TabPageConfFile.TabIndex = 2;
            TabPageConfFile.Text = "Configuration File";
            TabPageConfFile.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel1.BackColor = SystemColors.Control;
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(RichTextBoxConfFile);
            panel1.Location = new Point(10, 10);
            panel1.Margin = new Padding(10);
            panel1.Name = "panel1";
            panel1.Size = new Size(748, 349);
            panel1.TabIndex = 5;
            // 
            // RichTextBoxConfFile
            // 
            RichTextBoxConfFile.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            RichTextBoxConfFile.BorderStyle = BorderStyle.None;
            RichTextBoxConfFile.Location = new Point(-1, -1);
            RichTextBoxConfFile.Margin = new Padding(10);
            RichTextBoxConfFile.Name = "RichTextBoxConfFile";
            RichTextBoxConfFile.ReadOnly = true;
            RichTextBoxConfFile.ScrollBars = RichTextBoxScrollBars.Vertical;
            RichTextBoxConfFile.Size = new Size(748, 349);
            RichTextBoxConfFile.TabIndex = 0;
            RichTextBoxConfFile.Text = "";
            // 
            // ButtonClose
            // 
            ButtonClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            ButtonClose.Location = new Point(713, 415);
            ButtonClose.Name = "ButtonClose";
            ButtonClose.Size = new Size(75, 23);
            ButtonClose.TabIndex = 1;
            ButtonClose.Text = "Close";
            ButtonClose.UseVisualStyleBackColor = true;
            ButtonClose.Click += ButtonClose_Click;
            // 
            // FormHelp
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(ButtonClose);
            Controls.Add(TabControlHelp);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            MinimumSize = new Size(816, 489);
            Name = "FormHelp";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Help";
            Load += FormHelp_Load;
            TabControlHelp.ResumeLayout(false);
            TabPageManual.ResumeLayout(false);
            TabPageManual.PerformLayout();
            TabPageConfFile.ResumeLayout(false);
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TabControl TabControlHelp;
        private TabPage TabPageManual;
        private TabPage TabPageConfFileHelp;
        private TextBox TextBoxManual;
        private Button ButtonClose;
        private TextBox TextBoxConfFileHelp;
        private TabPage TabPageConfFile;
        private Panel panel1;
        private RichTextBox RichTextBoxConfFile;
    }
}