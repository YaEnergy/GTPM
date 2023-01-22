using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace GameTexturePackManager
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        public void ApplyLanguage()
        {
            Text = SettingsSystem.GetStringInLanguage("Settings_Name");
            ApplyButton.Text = SettingsSystem.GetStringInLanguage("Settings_Apply");
            languageLabel.Text = SettingsSystem.GetStringInLanguage("Settings_Language");
        }

        public static SettingsForm CreateSettingsDialog()
        {
            string[] languageCodes = SettingsSystem.GetLanguageNames();

            bool hasSettingsChanged = false;

            SettingsForm settingsForm = new();
            settingsForm.ApplyLanguage();

            void SaveSettings()
            {
                hasSettingsChanged = false;
                settingsForm.ApplyButton.Enabled = hasSettingsChanged;

                SettingsSystem.SetLanguage(CultureInfo.GetCultureInfo(languageCodes[settingsForm.LanguageComboBox.SelectedIndex]).TwoLetterISOLanguageName);

                settingsForm.ApplyLanguage();
                
                Exception? ex = DataFileSystem.WriteDataToTXTDataFile(SettingsSystem.SettingsFilePath, SettingsSystem.GetSettingsDictionary());

                if (ex != null) 
                    throw ex;

                MessageBox.Show(SettingsSystem.GetStringInLanguage("Settings_Saved"), SettingsSystem.GetStringInLanguage("Settings_Name"));
            }

            void OnSettingOptionChanged()
            {
                if (hasSettingsChanged) return;

                hasSettingsChanged = true;
                settingsForm.ApplyButton.Enabled = hasSettingsChanged;
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

            settingsForm.FormClosing += (object? s, FormClosingEventArgs args) 
                => OnClosing(s, args);

            settingsForm.ApplyButton.Click += (object? s, EventArgs args)
                => SaveSettings();

            settingsForm.LanguageComboBox.SelectionChangeCommitted += (object? s, EventArgs args)
                => OnSettingOptionChanged();

            for (int i = 0; i < languageCodes.Length; i++)
                settingsForm.LanguageComboBox.Items.Add(CultureInfo.GetCultureInfo(languageCodes[i]).NativeName);

            settingsForm.LanguageComboBox.SelectedIndex = settingsForm.LanguageComboBox.FindString(CultureInfo.GetCultureInfo(SettingsSystem.SelectedLanguageName).NativeName);

            return settingsForm;
        }
    }
}
