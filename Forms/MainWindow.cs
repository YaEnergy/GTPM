namespace GameTexturePackManager
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void ApplyLanguage()
        {
            ConfigureGameButton.Text = SettingsSystem.GetStringInLanguage("MainWindow_ConfigureGameButton");
            ResetDefaultTexturePackButton.Text = SettingsSystem.GetStringInLanguage("MainWindow_ResetDefaultTexturePackButton");
            OpenTexturePacksFolderButton.Text = SettingsSystem.GetStringInLanguage("MainWindow_OpenTexturePacksFolderButton");
            RefreshTexturePacksButton.Text = SettingsSystem.GetStringInLanguage("MainWindow_RefreshTexturePacksButton");
            ApplyTexturePacksButton.Text = SettingsSystem.GetStringInLanguage("MainWindow_ApplyTexturePacksButton");
            selectedTexturePacksLabel.Text = SettingsSystem.GetStringInLanguage("MainWindow_SelectedTexturePacksLabel");
            availableTPLabel.Text = SettingsSystem.GetStringInLanguage("MainWindow_AvailableTexturePacksLabel");
            selectedGameLabel.Text = SettingsSystem.GetStringInLanguage("MainWindow_SelectedGameLabel");
        }

        public void SetEnabledStateAllGameButtons(bool enabled)
        {
            SelectedTexturePacksList.Enabled = enabled;
            AvailableTexturePacksList.Enabled = enabled;
            ApplyTexturePacksButton.Enabled = enabled;
            OpenTexturePacksFolderButton.Enabled = enabled;
            RefreshTexturePacksButton.Enabled = enabled;
            ResetDefaultTexturePackButton.Enabled = enabled;
            ConfigureGameButton.Enabled = enabled;
        }
    }
}