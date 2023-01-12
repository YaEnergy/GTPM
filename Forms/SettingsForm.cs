using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameTexturePackManager
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        public void ApplyLanguage(Dictionary<string, string> languageDict)
        {
            Text = languageDict.ContainsKey("Settings_Name") ? languageDict["Settings_Name"] : SettingsSystem.DefaultLanguage["Settings_Name"];
            ApplyButton.Text = languageDict.ContainsKey("Settings_Apply") ? languageDict["Settings_Apply"] : SettingsSystem.DefaultLanguage["Settings_Apply"];
            languageLabel.Text = languageDict.ContainsKey("Settings_Language") ? languageDict["Settings_Language"] : SettingsSystem.DefaultLanguage["Settings_Language"];
        }

        public static SettingsForm CreateSettingsDialog()
        {
            bool hasSettingsChanged = false;

            SettingsForm settingsForm = new();
            settingsForm.ApplyLanguage(SettingsSystem.SelectedLanguage);

            void SaveSettings()
            {
                hasSettingsChanged = false;
                //Save Settings
                MessageBox.Show("Settings saved! TEST");
            }

            void OnClosing(object? s, FormClosingEventArgs args)
            {
                if (args.CloseReason != CloseReason.UserClosing)
                    return;

                if (!hasSettingsChanged)
                    return;

                DialogResult dialogResult = MessageBox.Show(SettingsSystem.GetStringInLanguage("Settings_AskToSave"), SettingsSystem.GetStringInLanguage("Settings_Name"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dialogResult == DialogResult.Yes)
                    SaveSettings();
            }

            settingsForm.FormClosing += (object? s, FormClosingEventArgs args) => OnClosing(s, args);

            settingsForm.ApplyButton.Click += (object? s, EventArgs args) => SaveSettings();

            string[] languageNames = SettingsSystem.GetLanguageNames();
            foreach (string languageName in languageNames)
                settingsForm.LanguageComboBox.Items.Add(languageName);



            //Display current settings and setting options

            return settingsForm;
        }
    }
}
