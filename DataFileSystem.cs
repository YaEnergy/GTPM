using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace GameTexturePackManager
{
    public static class DataFileSystem
    {
        public static Dictionary<string, string> GetDataFromTXTDataFile(FileInfo file)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            string[] keysPlusValues = File.ReadAllLines(file.FullName);

            for (int i = 0; i < keysPlusValues.Length; i += 2)
                if(i + 1 < keysPlusValues.Length)
                    data.Add(keysPlusValues[i], keysPlusValues[i + 1]);

            return data;
        }

        public static Exception? WriteDataToTXTDataFile(string filePath, Dictionary<string, string> data)
        {
            try
            {
                using StreamWriter sw = File.CreateText(filePath);
                foreach (KeyValuePair<string, string> kvp in data)
                {
                    sw.WriteLine(kvp.Key);
                    sw.WriteLine(kvp.Value);
                }
                sw.Close();
            }
            catch (Exception ex)
            {
                return ex;
            }
            return null;
        }

        public static Exception? OverwriteFile(FileInfo fileToOverwrite, FileInfo fileOverwriteWith)
        {
            try
            {
                FileStream fsToOverwrite = File.Create(fileToOverwrite.FullName);
                FileStream fsOverwriteWith = File.Open(fileOverwriteWith.FullName, FileMode.Open, FileAccess.ReadWrite);
                fsOverwriteWith.CopyTo(fsToOverwrite);
                fsToOverwrite.Close();
                fsOverwriteWith.Close();
                return null;
            }
            catch(Exception ex)
            {
                return ex;
            }
        }
        public static long GetDirectoryByteSize(DirectoryInfo dir)
        {
            long bytes = 0;

            foreach (DirectoryInfo subDir in dir.GetDirectories())
                bytes += GetDirectoryByteSize(subDir);

            foreach (FileInfo file in dir.GetFiles())
                bytes += file.Length;

            return bytes;
        }

        public static bool HasEnoughAvailableFreeSpace(long bytesRequired, string driveLetter = "C:")
        {
            DriveInfo[] driveInfo = DriveInfo.GetDrives();
            foreach (DriveInfo drive in driveInfo)
                if (driveInfo[0].Name == driveLetter)
                    return drive.AvailableFreeSpace > bytesRequired;

            return false;
        }

        public static int GetTotalFilesInDirectory(DirectoryInfo dir)
        {
            int totalFiles = 0;

            FileInfo[] files = dir.GetFiles();
            totalFiles += files.Length;

            DirectoryInfo[] directories = dir.GetDirectories();

            foreach(DirectoryInfo subDir in directories)
                totalFiles += GetTotalFilesInDirectory(subDir);

            return totalFiles;
        }
        public static int OvertakeFilesFromDirectory_Task(object? sender, DoWorkEventArgs e, DirectoryInfo dir, DirectoryInfo saveToDir, List<string> allowedExtensions, int totalFiles, int filesDone = 0)
        {
            if (sender == null)
                return filesDone;

            BackgroundWorker worker = (BackgroundWorker)sender;

            if (worker.CancellationPending)
            {
                e.Cancel = true;
                return filesDone;
            }

            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    return filesDone;
                }

                string copyingFilesFormat = SettingsSystem.GetStringInLanguage("CopyingFilesProgress");
                worker.ReportProgress(filesDone / totalFiles * 100, string.Format(copyingFilesFormat, file.Name, Math.Floor((double)filesDone / totalFiles * 100), filesDone, totalFiles));
                filesDone++;
                
                if (!allowedExtensions.Contains(file.Extension.ToLower()))
                    continue;

                FileStream fs = new FileStream(file.FullName, FileMode.Open, FileAccess.ReadWrite);
                FileStream newFs = File.Create(saveToDir.FullName + @$"\{file.Name}");
                fs.CopyTo(newFs);
                fs.Close();
                newFs.Close();
            }

            DirectoryInfo[] directories = dir.GetDirectories();
            foreach (DirectoryInfo directory in directories)
            {
                DirectoryInfo newDir = Directory.CreateDirectory(saveToDir.FullName + $@"\{directory.Name}");
                filesDone = OvertakeFilesFromDirectory_Task(sender, e, directory, newDir, allowedExtensions, totalFiles, filesDone);

                if (newDir.GetFiles().Length + newDir.GetDirectories().Length == 0)
                    newDir.Delete();
            }

            return filesDone;
        }

        public static string[] GetFileTypesWithFileExtensions(List<string> fileExtensions)
        {
            Dictionary<string, string> extensionTypes = GetDataFromTXTDataFile(new FileInfo(@"GTPMAssets\FileExtensions.txt"));
            List<string> fileTypes = new List<string>();
            foreach (KeyValuePair<string, string> kvp in extensionTypes)
            {
                string[] categoryFileExtensions = kvp.Value.Split(':');
                foreach (string extension in fileExtensions)
                    if (categoryFileExtensions.Contains(extension))
                    {
                        fileTypes.Add(kvp.Key);
                        break;
                    }
            }

            return fileTypes.ToArray();
        }
    }
}
