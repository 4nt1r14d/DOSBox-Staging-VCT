using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace dosbox_staging_vct
{
    public partial class FormSaveAs : Form
    {
        public FormSaveAs()
        {
            InitializeComponent();
        }

        private void FormSaveAs_Load(object sender, EventArgs e)
        {
            // Textbox only valid file characters
            TextBoxNewConfName.KeyPress += (sender, e) =>
            {
                if (e.KeyChar == '\\' || e.KeyChar == '/' || e.KeyChar == ':' || e.KeyChar == '*' || e.KeyChar == '?' || e.KeyChar == '"' || e.KeyChar == '<' || e.KeyChar == '>' || e.KeyChar == '|')
                {
                    e.Handled = true;
                }
            };
        }
    }
}
