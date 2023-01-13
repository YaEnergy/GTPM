﻿using System;
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
    public partial class AddGameForm : Form
    {
        public static List<string> AUTO_CHECKED_EXT = new() { "Image", "Audio", "Video", "3D Model", "Fonts" };
        public AddGameForm()
        {
            InitializeComponent();
            ResetFormControls();
        }

        public void ResetFormControls()
        {
            AllowedFileExtensionsCheckList.Items.Clear();

            Dictionary<string, string> extensionTypes = DataFileSystem.GetDataFromTXTDataFile(new FileInfo(@"GTPMAssets\FileExtensions.txt"));
            foreach (string ext in extensionTypes.Keys)
                AllowedFileExtensionsCheckList.Items.Add(ext, AUTO_CHECKED_EXT.Contains(ext));
        }

        private void SelectFolderButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "Select content folder";
            dialog.UseDescriptionForTitle = true;

            if(dialog.ShowDialog() == DialogResult.OK)
                ContentFolderPathTextBox.Text = dialog.SelectedPath;
        }

        public void ApplyLanguage()
        {
            gameNameLabel.Text = SettingsSystem.GetStringInLanguage("AddGameForm_GameNameLabel");
            contentFolderPathLabel.Text = SettingsSystem.GetStringInLanguage("AddGameForm_ContentFolderPathLabel");
            fileExtensionsChecklistLabel.Text = SettingsSystem.GetStringInLanguage("AddGameForm_AllowedFileTypesLabel");
            AddGameButton.Text = Text = SettingsSystem.GetStringInLanguage("AddGame");
            SelectFolderButton.Text = SettingsSystem.GetStringInLanguage("AddGameForm_SelectFolderButton");
        }

        private static AddGameForm CreateBaseAddGameForm()
        {
            AddGameForm addGameForm = new();

            addGameForm.ApplyLanguage();

            string[] presetGameFilePaths = Directory.GetFiles(@"GTPMAssets\Presets");
            List<CustomGame> foundPresetGames = new();
            foreach (string presetGameFilePath in presetGameFilePaths)
            {
                CustomGame presetGame = CustomGame.GetGameFromTXTDataFile(new FileInfo(presetGameFilePath));
                if (Directory.Exists(presetGame.FolderPath))
                    foundPresetGames.Add(presetGame);
            }

            if (foundPresetGames.Count > 0)
            {
                for (int i = 0; i < foundPresetGames.Count; i++)
                    addGameForm.GameNameTextBox.Items.Add(foundPresetGames[i].Name);

                addGameForm.GameNameTextBox.SelectionChangeCommitted += (sender, args) =>
                {
                    CustomGame game = foundPresetGames[addGameForm.GameNameTextBox.SelectedIndex];
                    addGameForm.ContentFolderPathTextBox.Text = game.FolderPath;

                    string[] fileTypesAllowed = DataFileSystem.GetFileTypesWithFileExtensions(game.AllowedExtensions);
                    for (int i = 0; i < addGameForm.AllowedFileExtensionsCheckList.Items.Count; i++)
                        addGameForm.AllowedFileExtensionsCheckList.SetItemChecked(i, fileTypesAllowed.Contains((string)addGameForm.AllowedFileExtensionsCheckList.Items[i]));
                };
            }
            else
                addGameForm.GameNameTextBox.Items.Add(SettingsSystem.GetStringInLanguage("NoPresetGamesFound"));

            return addGameForm;
        }

        public static AddGameForm CreateAddGameForm()
        {
            AddGameForm addGameForm = CreateBaseAddGameForm();

            addGameForm.Text = SettingsSystem.GetStringInLanguage("AddGame");
            addGameForm.GameNameTextBox.Enabled = true;
            addGameForm.GameNameTextBox.Text = "";
            addGameForm.ContentFolderPathTextBox.Text = "";

            for (int i = 0; i < addGameForm.AllowedFileExtensionsCheckList.Items.Count; i++)
                addGameForm.AllowedFileExtensionsCheckList.SetItemChecked(i, AUTO_CHECKED_EXT.Contains((string)addGameForm.AllowedFileExtensionsCheckList.Items[i]));

            return addGameForm;
        }

        public static AddGameForm CreateConfigureGameForm(CustomGame toConfigureGame)
        {
            AddGameForm addGameForm = CreateBaseAddGameForm();

            string configureGameFormatString = SettingsSystem.GetStringInLanguage("AddGameForm_ConfigureGameButton");
            addGameForm.Text = string.Format(configureGameFormatString, "Game");
            addGameForm.AddGameButton.Text = string.Format(configureGameFormatString, toConfigureGame.Name);
            addGameForm.GameNameTextBox.Text = toConfigureGame.Name;
            addGameForm.GameNameTextBox.Enabled = false;
            addGameForm.ContentFolderPathTextBox.Text = toConfigureGame.FolderPath;

            string[] fileTypesAllowed = DataFileSystem.GetFileTypesWithFileExtensions(toConfigureGame.AllowedExtensions);
            for (int i = 0; i < addGameForm.AllowedFileExtensionsCheckList.Items.Count; i++)
                addGameForm.AllowedFileExtensionsCheckList.SetItemChecked(i, fileTypesAllowed.Contains((string)addGameForm.AllowedFileExtensionsCheckList.Items[i]));

            return addGameForm;
        }
    }
}
