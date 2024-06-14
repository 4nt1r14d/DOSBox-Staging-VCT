using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Configuration;

namespace dosbox_staging_vct
{
    public partial class FormOptions : Form
    {
        private readonly string globalConfFileName = Properties.Settings.Default.GlobalConfFileName;
        private readonly string nonPortableFolderPath = Properties.Settings.Default.NonPortableFolderPathStart + Environment.UserName + Properties.Settings.Default.NonPortableFolderPathEnd;

        public FormOptions()
        {
            InitializeComponent();
        }

        private void FormOptions_Load(object sender, EventArgs e)
        {
            string dosBoxExeFilePath = Properties.Settings.Default.DosBoxStagingExeFilePath;

            if (string.IsNullOrEmpty(dosBoxExeFilePath))
            {
                SetInstallationAsPortable();
            }
            else if (InstallationIsPortable(dosBoxExeFilePath, globalConfFileName))
            {
                SetInstallationAsPortable();
            }
            else
            {
                SetInstallationAsNonPortable();
            }

            // load the paths from the settings
            TextBoxDosBoxStagingExeFilePath.Text = Properties.Settings.Default.DosBoxStagingExeFilePath;
            TextBoxGlobalConfFilePath.Text = Properties.Settings.Default.GlobalConfFilePath;
            TextBoxUserConfFolderPath.Text = Properties.Settings.Default.UserConfFolderPath;

            // load the parameters from the settings
            TextBoxParameters.Text = Properties.Settings.Default.LaunchParameters;
        }

        private void ButtonSetDosBoxStagingExeFilePath_Click(object sender, EventArgs e)
        {
            try
            {
                // Open the file dialog to select the DOSBox Staging executable
                string dosBoxExeFilePath = SelectDosBoxExePath();

                if (string.IsNullOrEmpty(dosBoxExeFilePath))
                {
                    throw new Exception("The DOSBox Staging executable is required to run the application. Please set the correct path.");
                }

                // Set the property and the TextBox to the selected file path
                SaveProperty("DosBoxStagingExeFilePath", dosBoxExeFilePath);
                TextBoxDosBoxStagingExeFilePath.Text = dosBoxExeFilePath;

                if (InstallationIsPortable(dosBoxExeFilePath, globalConfFileName))
                {
                    // Set the GlobalConfFilePath to the same folder as the DOSBox Staging executable
                    HandlePortableInstallation(dosBoxExeFilePath);
                }
                else
                {
                    // Set the GlobalConfFilePath to the no portable folder path
                    HandleNonPortableInstallation();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ButtonSetUserConfFolderPath_Click(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog folderBrowserDialog = new()
                {
                    Description = "Select the folder where the user configuration files are stored",
                    ShowNewFolderButton = true
                };

                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    string userConfFolderPath = folderBrowserDialog.SelectedPath;

                    // Set the property and the TextBox to the selected folder path
                    SaveProperty("UserConfFolderPath", userConfFolderPath);
                    TextBoxUserConfFolderPath.Text = userConfFolderPath;

                    // Load the user configuration files in the listbox of the FormMain
                    FormMain? formMain = (FormMain?)Application.OpenForms["FormMain"];
                    formMain?.LoadAllUserConfs();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ButtonMakeInstallationPortable_Click(object sender, EventArgs e)
        {

            try
            {
                string? dosBoxStagingFolderPath = Path.GetDirectoryName(Properties.Settings.Default.DosBoxStagingExeFilePath);

                if (dosBoxStagingFolderPath != null)
                {
                    // Copy all folders with its files from the non portable folder to the folder where the DOSBox Staging executable is located
                    string[] folders = Directory.GetDirectories(nonPortableFolderPath, "*", SearchOption.AllDirectories);

                    foreach (string folder in folders)
                    {
                        string folderName = Path.GetFileName(folder);
                        string destFolder = Path.Combine(dosBoxStagingFolderPath, folderName);
                        Directory.CreateDirectory(destFolder);

                        string[] filesInFolder = Directory.GetFiles(folder);

                        foreach (string file in filesInFolder)
                        {
                            string fileName = Path.GetFileName(file);
                            string destFile = Path.Combine(destFolder, fileName);
                            File.Copy(file, destFile, true);
                        }
                    }

                    // Copy all the files from the non portable folder to the folder where the DOSBox Staging executable is located
                    string[] files = Directory.GetFiles(nonPortableFolderPath);

                    foreach (string file in files)
                    {
                        string fileName = Path.GetFileName(file);
                        string destFile = Path.Combine(dosBoxStagingFolderPath, fileName);
                        File.Copy(file, destFile, true);
                    }

                    // Set the installation as portable
                    HandlePortableInstallation(Properties.Settings.Default.DosBoxStagingExeFilePath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void FormOptions_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Save the parameters to the settings
            if (TextBoxParameters.Text.Trim() != Properties.Settings.Default.LaunchParameters)
            {
                SaveProperty("LaunchParameters", TextBoxParameters.Text.Trim());
            }
        }

        private static bool InstallationIsPortable(string dosBoxStagingFilePath, string globalConfFileName)
        {
            string? dosBoxStagingFolderPath = Path.GetDirectoryName(dosBoxStagingFilePath);

            if (dosBoxStagingFolderPath != null)
            {
                // If there is a file called "dosbox-staging.conf" in the same folder as the dosBoxStagingFilePath, the installation is portable
                if (File.Exists(Path.Combine(dosBoxStagingFolderPath, globalConfFileName)))
                {
                    return true;
                }
            }
            return false;
        }

        private static string SelectDosBoxExePath()
        {
            OpenFileDialog openFileDialog = new()
            {
                Filter = "Executable files (*.exe)|*.exe",
                Title = "Select the DOSBox Staging executable",
                FileName = "dosbox.exe"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                return openFileDialog.FileName;
            }

            return string.Empty;
        }

        private void HandlePortableInstallation(string dosBoxExeFilePath)
        {
            SetInstallationAsPortable();

            string? dosBoxStagingFolderPath = Path.GetDirectoryName(dosBoxExeFilePath);
            if (dosBoxStagingFolderPath != null)
            {
                string newGlobalConfFilePath = Path.Combine(dosBoxStagingFolderPath, globalConfFileName);
                UpdateGlobalConfFilePath(newGlobalConfFilePath);
            }
        }

        private void HandleNonPortableInstallation()
        {
            SetInstallationAsNonPortable();

            string newGlobalConfFilePath = Path.Combine(nonPortableFolderPath, globalConfFileName);
            UpdateGlobalConfFilePath(newGlobalConfFilePath);
        }

        private void UpdateGlobalConfFilePath(string newGlobalConfFilePath)
        {
            SaveProperty("GlobalConfFilePath", newGlobalConfFilePath);
            TextBoxGlobalConfFilePath.Text = newGlobalConfFilePath;
        }

        private void SetInstallationAsPortable()
        {
            ButtonMakeInstallationPortable.Enabled = false;
            LabelInstallationPortable.Text = "The installation is portable.";
            LabelInstallationPortable.ForeColor = GlobalSettings.ColorHighlight;
        }

        private void SetInstallationAsNonPortable()
        {
            ButtonMakeInstallationPortable.Enabled = true;
            LabelInstallationPortable.Text = "The installation is not portable.";
            LabelInstallationPortable.ForeColor = GlobalSettings.ColorHighlight;
        }

        private static void SaveProperty(string propertyName, string propertyValue)
        {
            Properties.Settings.Default[propertyName] = propertyValue;
            Properties.Settings.Default.Save();
        }
    }
}
