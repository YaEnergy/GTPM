﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTexturePackManager
{
    public static class SettingsSystem
    {
        private static readonly string LanguageFolderPath = @"GTPMAssets\Languages\";

        public static string SelectedLanguageName = "-";
        public static Dictionary<string, string> SelectedLanguage = new();

        public static string DefaultLanguageName = "-";
        public static Dictionary<string, string> DefaultLanguage = new();

        public static string[] GetLanguageNames()
        {
            List<string> languageNames = new List<string>();
            FileInfo[] languageFiles = new DirectoryInfo(LanguageFolderPath).GetFiles();

            foreach (FileInfo languageFile in languageFiles)
                languageNames.Add(languageFile.Name);

            return languageNames.ToArray();
        }

        public static Dictionary<string, string> GetLanguageDictionary(string languageName)
        {
            if (!File.Exists(LanguageFolderPath + languageName + ".txt"))
                throw new Exception(languageName + " language doesn't exist!");

            return DataFileSystem.GetDataFromTXTDataFile(new FileInfo(LanguageFolderPath + languageName + ".txt"));
        }

        public static void SetLanguage(string languageName)
        {
            SelectedLanguageName = languageName;
            SelectedLanguage = DataFileSystem.GetDataFromTXTDataFile(new FileInfo(LanguageFolderPath + languageName + ".txt"));
        }

        public static void SetDefaultLanguage(string languageName)
        {
            DefaultLanguageName = languageName;
            DefaultLanguage = DataFileSystem.GetDataFromTXTDataFile(new FileInfo(LanguageFolderPath + languageName + ".txt"));
        }

        public static Dictionary<string, string> GetSettingsDictionary()
        {
            Dictionary<string, string> settings = new Dictionary<string, string>();
            settings.Add("SelectedLanguage", SelectedLanguageName);

            return settings;
        }
    }
}
