using System.ComponentModel;
using System.Diagnostics;

namespace GameTexturePackManager
{
    public class Program
    {
        private static Dictionary<string, string> GTPM_INFO = new();
        private static string GAMES_FOLDER_PATH = @"GTPMAssets\Games";
        private static Dictionary<string, string> EXTENSION_TYPES = new();

        private MainWindow mainWindow = new();
        private CustomGame? selectedGame = null;
        private Point startClickPointPos = Point.Empty;
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            GTPM_INFO = DataFileSystem.GetDataFromTXTDataFile(new FileInfo(@"GTPMAssets\GTPMInfo.txt"));
            EXTENSION_TYPES = DataFileSystem.GetDataFromTXTDataFile(new FileInfo(@"GTPMAssets\FileExtensions.txt"));
            SettingsSystem.SetDefaultLanguage("English");

            if (!Directory.Exists(GAMES_FOLDER_PATH))
                Directory.CreateDirectory(GAMES_FOLDER_PATH);

            if (!File.Exists(SettingsSystem.SettingsFilePath))
            {
                //Set default user settings
                SettingsSystem.SetLanguage("English"); //Find correct language (if exists) with cultureinfo

                DataFileSystem.WriteDataToTXTDataFile(SettingsSystem.SettingsFilePath, SettingsSystem.GetSettingsDictionary());
            }

            Dictionary<string, string> settingsDict = DataFileSystem.GetDataFromTXTDataFile(new FileInfo(SettingsSystem.SettingsFilePath));

            SettingsSystem.SetLanguage(settingsDict["SelectedLanguage"]);

            new Program().MainAsync().GetAwaiter().GetResult();
        }

        private void ListBox_DragStart(object? sender, MouseEventArgs args)
        {
            if (sender == null || args.Button != MouseButtons.Left)
                return;

            ListBox listbox = (ListBox)sender;
            startClickPointPos = listbox.IndexFromPoint(startClickPointPos) == -1 ? Point.Empty : args.Location;
        }

        private void ListBox_DragUpdate(object? sender, MouseEventArgs args, DragDropEffects dragEffect = DragDropEffects.Copy)
        {
            if (sender == null || args.Button != MouseButtons.Left)
                return;

            ListBox listbox = (ListBox)sender;
            if ((startClickPointPos != Point.Empty) && ((Math.Abs(args.X - startClickPointPos.X) > SystemInformation.DragSize.Width) || (Math.Abs(args.Y - startClickPointPos.Y) > SystemInformation.DragSize.Height)))
                listbox.DoDragDrop(sender, dragEffect);
        }

        private void ListBox_DragOver(object? sender, DragEventArgs args, DragDropEffects dragEffect = DragDropEffects.Copy)
            => args.Effect = dragEffect;

        private void RefreshSelectedGameDropdown()
        {
            mainWindow.selectedGameComboBox.Items.Clear();

            DirectoryInfo[] games = new DirectoryInfo(GAMES_FOLDER_PATH).GetDirectories();

            mainWindow.selectedGameComboBox.Items.Add(SettingsSystem.GetStringInLanguage("NoGameSelected"));
            mainWindow.selectedGameComboBox.SelectedIndex = 0;
            selectedGame = null;
            mainWindow.SetEnabledStateAllGameButtons(false);

            foreach (DirectoryInfo game in games)
                mainWindow.selectedGameComboBox.Items.Add(game.Name);

            mainWindow.selectedGameComboBox.Items.Add(SettingsSystem.GetStringInLanguage("AddGame"));
            RefreshTexturePacksList();
        }
        private void RefreshTexturePacksList()
        {
            mainWindow.SelectedTexturePacksList.Items.Clear();
            mainWindow.AvailableTexturePacksList.Items.Clear();
            mainWindow.SelectedTexturePacksList.Items.Add("Default");

            if (selectedGame == null)
                return;

            DirectoryInfo[] texturePacks = new DirectoryInfo(GAMES_FOLDER_PATH + $@"\{selectedGame.Name}\TexturePacks").GetDirectories();

            foreach (DirectoryInfo texturePack in texturePacks)
                if(texturePack.Name != "Default")
                    mainWindow.AvailableTexturePacksList.Items.Add(texturePack.Name);
        }
        private BackgroundWorker? CreateDefaultTexturePack(CustomGame game)
        {
            string defaultTexturePackName = SettingsSystem.GetStringInLanguage("DefaultTexturePack");
            DialogResult result = MessageBox.Show(string.Format(SettingsSystem.GetStringInLanguage("DefaultTexturePackQuestion"), game.Name, DataFileSystem.GetDirectoryByteSize(new DirectoryInfo(game.FolderPath)) / 1024 / 1024), defaultTexturePackName, MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result != DialogResult.OK)
                return null;

            if (!Directory.Exists(game.FolderPath))
                return null;

            mainWindow.SetEnabledStateAllGameButtons(false);
            mainWindow.selectedGameComboBox.Enabled = false;

            ProgressBarForm progressBarForm = new ProgressBarForm(SettingsSystem.GetStringInLanguage("CreateDefaultTexturePack"));
            progressBarForm.Show();
            mainWindow.AddOwnedForm(progressBarForm);

            Icon? windowIcon = Icon.ExtractAssociatedIcon(GTPM_INFO["IconPath"]);
            progressBarForm.Icon = windowIcon;

            string defaultTPPath = GAMES_FOLDER_PATH + @$"\{game.Name}\TexturePacks\Default";

            progressBarForm.BackgroundWorker.DoWork += (object? s, DoWorkEventArgs args) => DataFileSystem.OvertakeFilesFromDirectory_Task(s, args, new DirectoryInfo(game.FolderPath), Directory.CreateDirectory(defaultTPPath), game.AllowedExtensions, DataFileSystem.GetTotalFilesInDirectory(new DirectoryInfo(game.FolderPath)));
            progressBarForm.FormClosing += (object? s, FormClosingEventArgs args) =>
            {
                if (args.CloseReason != CloseReason.ApplicationExitCall)
                    progressBarForm.BackgroundWorker.CancelAsync();
            };
            progressBarForm.BackgroundWorker.RunWorkerCompleted += (object? s, RunWorkerCompletedEventArgs args) =>
            {
                progressBarForm.Close();

                if (args.Error != null)
                {
                    string defaultTexturePackExceptionFormat = SettingsSystem.GetStringInLanguage("DefaultTexturePackException");
                    MessageBox.Show(string.Format(defaultTexturePackExceptionFormat, game.Name, args.Error.Message), defaultTexturePackName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Directory.Delete(defaultTPPath, true);
                }
                else if (args.Cancelled)
                {
                    MessageBox.Show(SettingsSystem.GetStringInLanguage("DefaultTexturePackCancel"), defaultTexturePackName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Directory.Delete(defaultTPPath, true);
                }
                else
                    MessageBox.Show(string.Format(SettingsSystem.GetStringInLanguage("DefaultTexturePackSuccess"), game.Name), defaultTexturePackName, MessageBoxButtons.OK, MessageBoxIcon.Information);

                mainWindow.SetEnabledStateAllGameButtons(true);
                RefreshSelectedGameDropdown();
                mainWindow.selectedGameComboBox.Enabled = true;
            };
            progressBarForm.BackgroundWorker.RunWorkerAsync();
            
            return progressBarForm.BackgroundWorker;
        }
        private Exception? AddCustomGame(CustomGame game)
        {
            if (!Directory.Exists(game.FolderPath))
                return new Exception(string.Format(SettingsSystem.GetStringInLanguage("ContentFolderPathNotFound"), game.FolderPath));

            string newGameFolderPath = GAMES_FOLDER_PATH + $@"\{game.Name}";
            if (!Directory.Exists(newGameFolderPath))
                Directory.CreateDirectory(newGameFolderPath);

            if (!Directory.Exists(newGameFolderPath + @"\TexturePacks"))
                Directory.CreateDirectory(newGameFolderPath + @"\TexturePacks");

            Exception? dataFileException = DataFileSystem.WriteDataToTXTDataFile(@$"{newGameFolderPath}\gtmADATA.txt", game.ToDictionary());
            if (dataFileException != null)
                return dataFileException;

            long bytesReq = DataFileSystem.GetDirectoryByteSize(new DirectoryInfo(game.FolderPath));

            if (!DataFileSystem.HasEnoughAvailableFreeSpace(bytesReq, Directory.GetDirectoryRoot(GAMES_FOLDER_PATH)))
            {
                MessageBox.Show(string.Format(SettingsSystem.GetStringInLanguage("DefaultTexturePackNotEnoughSpaceError"), bytesReq / 1024 / 1024), SettingsSystem.GetStringInLanguage("CustomGameCreationError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            else
                CreateDefaultTexturePack(game);

            RefreshSelectedGameDropdown();

            return null;
        }

        private void SelectGame(int index)
        {
            string gameName = (string)mainWindow.selectedGameComboBox.SelectedItem;
            bool isGameIndex = index != 0 && index != mainWindow.selectedGameComboBox.Items.Count - 1 && File.Exists(GAMES_FOLDER_PATH + $@"\{gameName}\gtmADATA.txt");

            mainWindow.SetEnabledStateAllGameButtons(isGameIndex);

            if (!isGameIndex)
                selectedGame = null;
            else
                selectedGame = CustomGame.GetGameFromTXTDataFile(new FileInfo(GAMES_FOLDER_PATH + $@"\{gameName}\gtmADATA.txt"));

            RefreshTexturePacksList();

            if (index == mainWindow.selectedGameComboBox.Items.Count - 1)
            {
                RefreshSelectedGameDropdown();

                AddGameForm addGameForm = AddGameForm.CreateAddGameForm();
                addGameForm.Icon = mainWindow.Icon;

                addGameForm.AddGameButton.Click += (object? s, EventArgs args)
                    => OnClickAddGameButton(addGameForm);

                addGameForm.ShowDialog();
                return;
            }

            if (!Directory.Exists(GAMES_FOLDER_PATH + $@"\{gameName}\TexturePacks\Default") && isGameIndex && selectedGame != null)
            {
                mainWindow.SetEnabledStateAllGameButtons(false);
                CreateDefaultTexturePack(selectedGame);
            }
        }

        private void SelectTexturePack(object? sender, DragEventArgs args)
        {
            if (sender == null)
                return;

            ListBox listbox = (ListBox)sender;

            Point newPoint = new Point(args.X, args.Y);
            newPoint = listbox.PointToClient(newPoint);

            int selectedIndex = listbox.IndexFromPoint(newPoint);
            int texturePackIndex = mainWindow.AvailableTexturePacksList.IndexFromPoint(startClickPointPos);
            if (texturePackIndex == -1)
                return;

            object item = mainWindow.AvailableTexturePacksList.Items[texturePackIndex];

            if (mainWindow.SelectedTexturePacksList.Items.Contains(item))
                return;

            if (selectedIndex == -1)
                listbox.Items.Add(item);
            else if (selectedIndex == 0)
                listbox.Items.Insert(1, item);
            else
                listbox.Items.Insert(selectedIndex, item);
        }

        private void RemoveSelectedTexturePack(object? sender, MouseEventArgs args)
        {
            if (sender == null)
                return;

            ListBox listbox = (ListBox)sender;

            int selectedIndex = listbox.IndexFromPoint(args.Location);
            if (selectedIndex == -1)
                return;

            object item = listbox.Items[selectedIndex];
            if (item is string)
                if((string)item == "Default")
                    return;

            listbox.Items.RemoveAt(selectedIndex);
        }

        private int ApplyTexturePacksToGameTask(object? s, DoWorkEventArgs args, CustomGame game, string[] texturePacks, int totalFiles, int filesDone = 0, string subFolder = "")
        {
            if (s == null)
                return filesDone;

            BackgroundWorker worker = (BackgroundWorker)s;
            string selectedGameFolder = GAMES_FOLDER_PATH + @$"\{game.Name}";
            string texturePacksFolder = selectedGameFolder + @"\TexturePacks";

            DirectoryInfo gameFolder = new DirectoryInfo(game.FolderPath + subFolder);
            FileInfo[] files = gameFolder.GetFiles();

            if (worker.CancellationPending)
            {
                args.Cancel = true;
                return filesDone;
            }

            foreach (FileInfo file in files)
            {
                if (worker.CancellationPending)
                {
                    args.Cancel = true;
                    return filesDone;
                }

                if (!game.AllowedExtensions.Contains(file.Extension))
                    continue;

                string overwritingFilesFormat = SettingsSystem.GetStringInLanguage("OverwritingFilesProgress");
                worker.ReportProgress(filesDone / totalFiles * 100, string.Format(overwritingFilesFormat, file.Name, Math.Floor((double)filesDone / totalFiles * 100), filesDone, totalFiles));
                filesDone++;

                string filePathInTexturePack = file.FullName.Replace(game.FolderPath, "");
                bool textureFound = false;

                for (int tp = texturePacks.Length - 1; tp >= 0; tp--)
                    if (File.Exists(@$"{texturePacksFolder}\{texturePacks[tp]}{filePathInTexturePack}"))
                    {
                        Exception? ex = DataFileSystem.OverwriteFile(file, new FileInfo(@$"{texturePacksFolder}\{texturePacks[tp]}{filePathInTexturePack}"));
                        if (ex != null)
                            throw ex;

                        textureFound = true;
                        break;
                    }

                if (!textureFound)
                    throw new Exception(string.Format(SettingsSystem.GetStringInLanguage("TextureNotFoundException"), filePathInTexturePack));
            }

            DirectoryInfo[] directories = gameFolder.GetDirectories();
            foreach (DirectoryInfo directory in directories)
                filesDone = ApplyTexturePacksToGameTask(s, args, game, texturePacks, totalFiles, filesDone, subFolder + @$"\{directory.Name}");

            return filesDone;
        }

        private void OnClickAddGameButton(AddGameForm addGameForm)
        {
            if (addGameForm.GameNameTextBox.Text == "" || addGameForm.ContentFolderPathTextBox.Text == "")
                return;

            if (addGameForm.GameNameTextBox.Text.IndexOfAny(Path.GetInvalidFileNameChars()) != -1)
            {
                string invalidPathCharsString = "";
                foreach (char c in Path.GetInvalidFileNameChars())
                    invalidPathCharsString += c;

                MessageBox.Show(SettingsSystem.GetStringInLanguage("InvalidGameName"));
                return;
            }

            DialogResult dialogResult = MessageBox.Show(string.Format(SettingsSystem.GetStringInLanguage("AddGameQuestion"), addGameForm.GameNameTextBox.Text), SettingsSystem.GetStringInLanguage("AddGame"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult != DialogResult.Yes)
                return;

            if (addGameForm.TexturePackUpToDateCheckbox.Checked)
                MessageBox.Show(SettingsSystem.GetStringInLanguage("UpToDateSettingNote"), SettingsSystem.GetStringInLanguage("AddGame"), MessageBoxButtons.OK, MessageBoxIcon.Information);

            CustomGame game = new CustomGame();
            game.Name = addGameForm.GameNameTextBox.Text;
            game.FolderPath = addGameForm.ContentFolderPathTextBox.Text;
            game.CheckIfUpToDate = addGameForm.TexturePackUpToDateCheckbox.Checked;
            foreach (string key in addGameForm.AllowedFileExtensionsCheckList.CheckedItems)
                foreach (string extension in EXTENSION_TYPES[SettingsSystem.DefaultLanguage[SettingsSystem.GetKeyFromStringInLanguage(key, SettingsSystem.SelectedLanguage)]].Split(':'))
                    game.AllowedExtensions.Add(extension.ToLower());

            addGameForm.Close();
            Exception? ex = AddCustomGame(game);
            if (ex != null)
            {
                MessageBox.Show(ex.Message, SettingsSystem.GetStringInLanguage("CustomGameCreationError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                string path = GAMES_FOLDER_PATH + $@"\{game.Name}";
                if (Directory.Exists(path) && Directory.GetDirectories(path).Length == 0 && Directory.GetFiles(path).Length == 0)
                {
                    Directory.Delete(path);
                    MessageBox.Show(string.Format(SettingsSystem.GetStringInLanguage("GameDirectoryRemoved"), GAMES_FOLDER_PATH, game.Name), SettingsSystem.GetStringInLanguage("CustomGameCreationError"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private BackgroundWorker? ApplySelectedTexturePacksToGame(CustomGame game, bool askQuestion = true)
        {
            mainWindow.SetEnabledStateAllGameButtons(false);
            mainWindow.selectedGameComboBox.Enabled = false;

            // Check if default texture pack is up-to-date
            if (game.CheckIfUpToDate)
                if (!game.IsDefaultTexturePackUpToDate())
                {
                    DialogResult result = MessageBox.Show(SettingsSystem.GetStringInLanguage("DefaultTexturePackNotUpToDateQuestion"), SettingsSystem.GetStringInLanguage("DefaultTexturePack"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    
                    if (result == DialogResult.Yes)
                        CreateDefaultTexturePack(game);
                    else
                    {
                        mainWindow.SetEnabledStateAllGameButtons(true);
                        mainWindow.selectedGameComboBox.Enabled = true;
                    }

                    return null;
                }

            string applyTexturePacksString = SettingsSystem.GetStringInLanguage("ApplyTexturePacks");
            List<string> texturePacks = new();
            long bytesRequired = 0;
            for (int i = 0; i < mainWindow.SelectedTexturePacksList.Items.Count; i++)
                if (mainWindow.SelectedTexturePacksList.Items[i] is string)
                {
                    texturePacks.Add((string)mainWindow.SelectedTexturePacksList.Items[i]);
                    bytesRequired += DataFileSystem.GetDirectoryByteSize(new DirectoryInfo(GAMES_FOLDER_PATH + $@"\{game.Name}\TexturePacks\{mainWindow.SelectedTexturePacksList.Items[i]}"));
                }

            if(!DataFileSystem.HasEnoughAvailableFreeSpace(bytesRequired, Directory.GetDirectoryRoot(GAMES_FOLDER_PATH)))
            {
                MessageBox.Show(string.Format(SettingsSystem.GetStringInLanguage("ApplyTexturePackNotEnoughSpaceError"), bytesRequired / 1024 / 1024), applyTexturePacksString, MessageBoxButtons.OK, MessageBoxIcon.Error);
                mainWindow.SetEnabledStateAllGameButtons(true);
                mainWindow.selectedGameComboBox.Enabled = true;
                return null;
            }

            if(askQuestion)
            {
                DialogResult result = MessageBox.Show(SettingsSystem.GetStringInLanguage("ApplyTexturePackQuestion"), applyTexturePacksString, MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (result != DialogResult.OK)
                {
                    mainWindow.SetEnabledStateAllGameButtons(true);
                    mainWindow.selectedGameComboBox.Enabled = true;
                    return null;
                }
            }

            if (!Directory.Exists(game.FolderPath))
            {
                mainWindow.SetEnabledStateAllGameButtons(true);
                mainWindow.selectedGameComboBox.Enabled = true;
                return null;
            }

            ProgressBarForm progressBarForm = new ProgressBarForm(applyTexturePacksString);
            progressBarForm.Show();
            mainWindow.AddOwnedForm(progressBarForm);

            progressBarForm.Icon = mainWindow.Icon;

            progressBarForm.BackgroundWorker.DoWork += (object? s, DoWorkEventArgs args)
                => ApplyTexturePacksToGameTask(s, args, game, texturePacks.ToArray(), DataFileSystem.GetTotalFilesInDirectory(new DirectoryInfo(GAMES_FOLDER_PATH + @$"\{game.Name}\TexturePacks\Default")));
            
            progressBarForm.FormClosing += (object? s, FormClosingEventArgs args) =>
            {
                if (args.CloseReason != CloseReason.ApplicationExitCall)
                    progressBarForm.BackgroundWorker.CancelAsync();
            };

            progressBarForm.BackgroundWorker.RunWorkerCompleted += (object? s, RunWorkerCompletedEventArgs args) =>
            {
                progressBarForm.Close();

                if (args.Error != null)
                    MessageBox.Show(string.Format(SettingsSystem.GetStringInLanguage("ApplyException"), game.Name, args.Error.Message), applyTexturePacksString, MessageBoxButtons.OK, MessageBoxIcon.Error);
                else if (args.Cancelled)
                    MessageBox.Show(SettingsSystem.GetStringInLanguage("ApplyCancel"), applyTexturePacksString, MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show(string.Format(SettingsSystem.GetStringInLanguage("ApplySuccess"), game.Name), applyTexturePacksString, MessageBoxButtons.OK, MessageBoxIcon.Information);


                mainWindow.SetEnabledStateAllGameButtons(true);
                RefreshSelectedGameDropdown(); //Only selected texture pack will be Default
                mainWindow.selectedGameComboBox.Enabled = true;

                if (args.Error != null || args.Cancelled)
                {
                    DialogResult result = MessageBox.Show(SettingsSystem.GetStringInLanguage("ApplyDefaultQuestion"), applyTexturePacksString, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if(result == DialogResult.Yes)
                        ApplySelectedTexturePacksToGame(game, false);
                }
            };
            progressBarForm.BackgroundWorker.RunWorkerAsync();

            return progressBarForm.BackgroundWorker;
        }

        public async Task MainAsync()
        {
            mainWindow.Text = $"Game Texture Pack Manager ({GTPM_INFO["Version"]})";
            mainWindow.ApplyLanguage();

            Icon? windowIcon = Icon.ExtractAssociatedIcon(GTPM_INFO["IconPath"]);
            mainWindow.Icon = windowIcon;

            RefreshSelectedGameDropdown();
            mainWindow.RefreshTexturePacksButton.Click += (object? s, EventArgs args) 
                => RefreshTexturePacksList();

            mainWindow.OpenTexturePacksFolderButton.Click += (object? s, EventArgs args) => 
            {
                if (selectedGame != null)
                    Process.Start("explorer.exe", GAMES_FOLDER_PATH + @$"\{selectedGame.Name}\TexturePacks");
            };

            mainWindow.selectedGameComboBox.SelectionChangeCommitted += (object? s, EventArgs args) 
                => SelectGame(mainWindow.selectedGameComboBox.SelectedIndex);

            mainWindow.ResetDefaultTexturePackButton.Click += (object? s, EventArgs args) =>
            {
                if (selectedGame != null)
                    CreateDefaultTexturePack(selectedGame);
            };

            mainWindow.AvailableTexturePacksList.MouseDown += (object? s, MouseEventArgs args)
                => ListBox_DragStart(s, args);

            mainWindow.AvailableTexturePacksList.MouseMove += (object? s, MouseEventArgs args) 
                => ListBox_DragUpdate(s, args, DragDropEffects.Move);

            mainWindow.SelectedTexturePacksList.DragOver += (object? s, DragEventArgs args)
                => ListBox_DragOver(s, args, DragDropEffects.Move);

            mainWindow.SelectedTexturePacksList.DragDrop += (object? s, DragEventArgs args)
                => SelectTexturePack(s, args);

            mainWindow.SelectedTexturePacksList.MouseDoubleClick += (object? s, MouseEventArgs args)
                => RemoveSelectedTexturePack(s, args);

            mainWindow.ApplyTexturePacksButton.Click += (object? s, EventArgs args) =>
            {
                if (selectedGame != null)
                    ApplySelectedTexturePacksToGame(selectedGame);
            };

            mainWindow.ConfigureGameButton.Click += (object? s, EventArgs args) =>
            {
                if (selectedGame == null) 
                    return;

                AddGameForm addGameForm = AddGameForm.CreateConfigureGameForm(selectedGame);
                addGameForm.Icon = mainWindow.Icon;

                addGameForm.AddGameButton.Click += (object? s, EventArgs args)
                    => OnClickAddGameButton(addGameForm);

                addGameForm.ShowDialog();
            };

            mainWindow.OpenSettingsButton.Click += (object? s, EventArgs args) =>
            {
                SettingsForm settingsForm = SettingsForm.CreateSettingsDialog();
                settingsForm.Icon = mainWindow.Icon;
                settingsForm.ShowDialog();

                mainWindow.ApplyLanguage();
                RefreshSelectedGameDropdown();
            };

            SelectGame(0);

            Application.Run(mainWindow);
        }
    }
}