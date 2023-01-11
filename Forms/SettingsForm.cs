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
    }
}
