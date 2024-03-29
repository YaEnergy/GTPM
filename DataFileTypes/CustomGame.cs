﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTexturePackManager
{
    public class CustomGame
    {
        public string Name { get; set; } = "";
        public string FolderPath { get; set; } = "";
        public List<string> AllowedExtensions { get; set; } = new();
        public bool CheckIfUpToDate = true;


        /// <summary>
        /// Returns a CustomGame Object from a TXTDataFile that represents a Preset or Custom game
        /// </summary>
        /// <returns>CustomGame</returns>
        public static CustomGame GetGameFromTXTDataFile(FileInfo file)
        {
            CustomGame game = new CustomGame();
            Dictionary<string, string> data = DataFileSystem.GetDataFromTXTDataFile(file);
            game.Name = data.ContainsKey("GameName") ? data["GameName"] : "null";
            game.FolderPath = data.ContainsKey("FolderPath") ? data["FolderPath"] : "null";
            game.CheckIfUpToDate = data.ContainsKey("CheckIfUpToDate") ? bool.Parse(data["CheckIfUpToDate"]) : true;

            if (data.ContainsKey("AllowedExtensions"))
            {
                string[] allowedExtensions = data["AllowedExtensions"].Split(':');
                foreach(string extension in allowedExtensions)
                    game.AllowedExtensions.Add(extension);
            }

            return game;
        }

        public int GetTotalAllowedFilesInDirectory(DirectoryInfo dir, int numFiles = 0)
        {
            foreach (FileInfo file in dir.GetFiles())
                if (AllowedExtensions.Contains(file.Extension))
                    numFiles++;

            foreach (DirectoryInfo subDir in dir.GetDirectories())
                numFiles = GetTotalAllowedFilesInDirectory(subDir, numFiles);

            return numFiles;
        }
        public bool IsDefaultTexturePackUpToDate()
        {
            int totalDefaultTexturePackFiles = GetTotalAllowedFilesInDirectory(new DirectoryInfo($@"GTPMAssets\Games\{Name}\TexturePacks\Default"));
            int totalAllowedGameFiles = GetTotalAllowedFilesInDirectory(new DirectoryInfo(FolderPath));

            return totalDefaultTexturePackFiles >= totalAllowedGameFiles;
        }

        public Dictionary<string, string> ToDictionary()
        {
            Dictionary<string, string> data = new();
            data.Add("GameName", Name);
            data.Add("FolderPath", FolderPath);
            data.Add("CheckIfUpToDate", CheckIfUpToDate.ToString());

            string allowedExtensionsString = "";
            for (int i = 0; i < AllowedExtensions.Count; i++)
                allowedExtensionsString += (i == AllowedExtensions.Count - 1) ? AllowedExtensions[i] : AllowedExtensions[i] + ":";

            data.Add("AllowedExtensions", allowedExtensionsString);

            return data;
        }
    }
}
