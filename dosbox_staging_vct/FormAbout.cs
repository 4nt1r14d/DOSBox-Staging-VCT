using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace dosbox_staging_vct
{
    public partial class FormAbout : Form
    {
        public FormAbout()
        {
            InitializeComponent();
        }

        private void FormAbout_Load(object sender, EventArgs e)
        {
            // Set the color of the LabelVersion
            LabelVersion.ForeColor = GlobalSettings.ColorHighlight;
            // Set the version number in the LabelVersion
            LabelVersion.Text = "v." + Application.ProductVersion;

            // Set the color of the link labels
            LinkLabelDosboxStaging.LinkColor = GlobalSettings.ColorHighlight;
            LinkLabelDosboxStaging.ActiveLinkColor = GlobalSettings.ColorHighlight;
            LinkLabelDosboxStaging.VisitedLinkColor = GlobalSettings.ColorHighlight;

            LinkLabelGitHubPage.LinkColor = GlobalSettings.ColorHighlight;
            LinkLabelGitHubPage.ActiveLinkColor = GlobalSettings.ColorHighlight;
            LinkLabelGitHubPage.VisitedLinkColor = GlobalSettings.ColorHighlight;
        }

        private void LinkLabelDosboxStaging_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                // Open the URL in the default browser
                System.Diagnostics.Process.Start(new ProcessStartInfo("https://dosbox-staging.github.io/") { UseShellExecute = true });
            }
            catch (System.ComponentModel.Win32Exception noBrowser)
            {
                if (noBrowser.ErrorCode == -2147467259)
                    MessageBox.Show(noBrowser.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (System.Exception other)
            {
                MessageBox.Show(other.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LinkLabelGitHubPage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                // Open the URL in the default browser
                System.Diagnostics.Process.Start(new ProcessStartInfo("https://github.com/4nt1r14d/DOSBox-Staging-VCT/") { UseShellExecute = true });
            }
            catch (System.ComponentModel.Win32Exception noBrowser)
            {
                if (noBrowser.ErrorCode == -2147467259)
                    MessageBox.Show(noBrowser.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (System.Exception other)
            {
                MessageBox.Show(other.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ButtonClose_Click(object sender, EventArgs e)
        {
            // Close the form
            Close();
        }
    }
}
