using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTexturePackManager
{
    public static class SettingsSystem
    {
        public static readonly string SettingsFilePath = @"GTPMAssets\GTPMSettings.txt";
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
                languageNames.Add(languageFile.Name.Replace(languageFile.Extension, ""));

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

        public static string GetKeyFromStringInLanguage(string Text, Dictionary<string, string> languageDict)
        {
            foreach(KeyValuePair<string, string> kvp in languageDict)
                if(kvp.Value == Text)
                    return kvp.Key;

            throw new Exception("Text not found");
        }

        public static string GetStringInLanguage(string key, Dictionary<string, string> languageDict)
        {
            string Value = languageDict.ContainsKey(key) ? languageDict[key] : DefaultLanguage[key];

            return Value.Replace("<br>", "\n");
        }

        public static string GetStringInLanguage(string key)
        {
            string Value = SelectedLanguage.ContainsKey(key) ? SelectedLanguage[key] : DefaultLanguage[key];

            return Value.Replace("<br>", "\n");
        }
        public static Dictionary<string, string> GetSettingsDictionary()
        {
            Dictionary<string, string> settings = new Dictionary<string, string>();
            settings.Add("SelectedLanguage", SelectedLanguageName);

            return settings;
        }
    }
}
