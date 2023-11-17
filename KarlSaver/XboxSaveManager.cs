using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarlSaver
{
    public static class XboxSaveManager
    {
        public static string GetSaveFolderPath()
        {
            string _win10Path = @"C:\Users\" + Environment.UserName + @"\AppData\Local\Packages\";
            string[] win10Games = Directory.GetDirectories(_win10Path);

            var validFolders = win10Games.Where(s => s.Contains("CoffeeStainStudios.DeepRockGalactic"));

            if (!validFolders.Any())
                return null;

            return validFolders.First();
        }

        public static string GetSaveFilePath(string xboxCoffeeStainFolderPath)
        {
            var initialPath = xboxCoffeeStainFolderPath + @"\SystemAppData\wgs\";
            var saveFilePath = SearchSaveFile(initialPath);
            return saveFilePath;
        }

        private static string SearchSaveFile(string initialPathToSearch)
        {
            int minNameLength = 32; // GUID non-hyphenated length
            var folders = Directory.GetDirectories(initialPathToSearch).Where(x => Path.GetFileName(x).Length >= minNameLength).ToArray();
            if (folders.Length == 1)
                return SearchSaveFile(folders.First());

            foreach (var folder in folders)
            {
                if (!Guid.TryParse(folder, out Guid result))
                    continue;

                return SearchSaveFile(folder);
            }

            var files = Directory.GetFiles(initialPathToSearch).Where(x => Path.GetFileName(x).Length >= minNameLength);
            return files.FirstOrDefault();
        }
    }
}
