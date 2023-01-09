namespace GameTexturePackManager
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void ApplyLanguage(Dictionary<string, string> languageDict)
        {
            ConfigureGameButton.Text = languageDict.ContainsKey("MainWindow_ConfigureGameButton") ? languageDict["MainWindow_ConfigureGameButton"] : SettingsSystem.DefaultLanguage["MainWindow_ConfigureGameButton"];
            ResetDefaultTexturePackButton.Text = languageDict.ContainsKey("MainWindow_ResetDefaultTexturePackButton") ? languageDict["MainWindow_ResetDefaultTexturePackButton"] : SettingsSystem.DefaultLanguage["MainWindow_ResetDefaultTexturePackButton"];
            OpenTexturePacksFolderButton.Text = languageDict.ContainsKey("MainWindow_OpenTexturePacksFolderButton") ? languageDict["MainWindow_OpenTexturePacksFolderButton"] : SettingsSystem.DefaultLanguage["MainWindow_OpenTexturePacksFolderButton"];
            RefreshTexturePacksButton.Text = languageDict.ContainsKey("MainWindow_RefreshTexturePacksButton") ? languageDict["MainWindow_RefreshTexturePacksButton"] : SettingsSystem.DefaultLanguage["MainWindow_RefreshTexturePacksButton"];
            ApplyTexturePacksButton.Text = languageDict.ContainsKey("MainWindow_ApplyTexturePacksButton") ? languageDict["MainWindow_ApplyTexturePacksButton"] : SettingsSystem.DefaultLanguage["MainWindow_ApplyTexturePacksButton"];
            selectedTexturePacksLabel.Text = languageDict.ContainsKey("MainWindow_SelectedTexturePacksLabel") ? languageDict["MainWindow_SelectedTexturePacksLabel"] : SettingsSystem.DefaultLanguage["MainWindow_SelectedTexturePacksLabel"];
            availableTPLabel.Text = languageDict.ContainsKey("MainWindow_AvailableTexturePacksLabel") ? languageDict["MainWindow_AvailableTexturePacksLabel"] : SettingsSystem.DefaultLanguage["MainWindow_AvailableTexturePacksLabel"];
            selectedGameLabel.Text = languageDict.ContainsKey("MainWindow_SelectedGameLabel") ? languageDict["MainWindow_SelectedGameLabel"] : SettingsSystem.DefaultLanguage["MainWindow_SelectedGameLabel"];
        }
    }
}