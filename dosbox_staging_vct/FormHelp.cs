using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dosbox_staging_vct
{
    public partial class FormHelp : Form
    {
        public FormHelp()
        {
            InitializeComponent();
        }

        private void FormHelp_Load(object sender, EventArgs e)
        {
            try
            {
                // Load the DOSBox official Manual from the help folder of the application
                string helpText = System.IO.File.ReadAllText("help\\manual.txt");
                TextBoxManual.Text = helpText;

                // Load the DOSBox official Configuration file from the help folder of the application
                string confFileText = System.IO.File.ReadAllText("help\\dosbox-staging.conf");
                TextBoxConfFile.Text = confFileText;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void ButtonClose_Click(object sender, EventArgs e)
        {
            // Close the form
            this.Close();
        }
    }
}
