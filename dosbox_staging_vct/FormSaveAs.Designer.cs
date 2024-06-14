namespace dosbox_staging_vct
{
    partial class FormSaveAs
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
            ButtonOk = new Button();
            ButtonCancel = new Button();
            TextBoxNewConfName = new TextBox();
            LabelTitle = new Label();
            SuspendLayout();
            // 
            // ButtonOk
            // 
            ButtonOk.Location = new Point(232, 69);
            ButtonOk.Name = "ButtonOk";
            ButtonOk.Size = new Size(75, 23);
            ButtonOk.TabIndex = 2;
            ButtonOk.Text = "Ok";
            ButtonOk.UseVisualStyleBackColor = true;
            // 
            // ButtonCancel
            // 
            ButtonCancel.Location = new Point(313, 69);
            ButtonCancel.Name = "ButtonCancel";
            ButtonCancel.Size = new Size(75, 23);
            ButtonCancel.TabIndex = 3;
            ButtonCancel.Text = "Cancel";
            ButtonCancel.UseVisualStyleBackColor = true;
            // 
            // TextBoxNewConfName
            // 
            TextBoxNewConfName.Location = new Point(12, 38);
            TextBoxNewConfName.Name = "TextBoxNewConfName";
            TextBoxNewConfName.Size = new Size(376, 23);
            TextBoxNewConfName.TabIndex = 1;
            // 
            // LabelTitle
            // 
            LabelTitle.AutoSize = true;
            LabelTitle.Location = new Point(12, 15);
            LabelTitle.Name = "LabelTitle";
            LabelTitle.Size = new Size(243, 15);
            LabelTitle.TabIndex = 0;
            LabelTitle.Text = "Enter the name of the new configuration file:";
            // 
            // FormSaveAs
            // 
            AcceptButton = ButtonOk;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = ButtonCancel;
            ClientSize = new Size(400, 104);
            ControlBox = false;
            Controls.Add(LabelTitle);
            Controls.Add(TextBoxNewConfName);
            Controls.Add(ButtonCancel);
            Controls.Add(ButtonOk);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormSaveAs";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Save as...";
            Load += FormSaveAs_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button ButtonOk;
        private Button ButtonCancel;
        private TextBox TextBoxNewConfName;
        private Label LabelTitle;
    }
}