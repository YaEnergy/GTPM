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

        public void ApplyLanguage(Dictionary<string, string> languageDict)
        {
            gameNameLabel.Text = "";
            contentFolderPathLabel.Text = "";
            fileExtensionsChecklistLabel.Text = "";
            AddGameButton.Text = "";
        }

        private static AddGameForm CreateBaseAddGameForm()
        {
            AddGameForm addGameForm = new();

            return addGameForm;
        }

        public static AddGameForm CreateAddGameForm()
        {
            AddGameForm addGameForm = CreateBaseAddGameForm();

            //WIP

            return addGameForm;
        }

        public static AddGameForm CreateConfigureGameForm()
        {
            AddGameForm addGameForm = CreateBaseAddGameForm();

            //WIP

            return addGameForm;
        }
    }
}
