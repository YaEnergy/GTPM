using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTexturePackManager
{
    public static class LanguageSystem
    {
        private static readonly string LanguageFolderPath = @"GTPMAssets\Languages\";

        public static string SelectedLanguageName = "-";
        public static Dictionary<string, string> SelectedLanguage = new();

        public static string DefaultLanguageName = "-";
        public static Dictionary<string, string> DefaultLanguage = new();

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
    }
}
