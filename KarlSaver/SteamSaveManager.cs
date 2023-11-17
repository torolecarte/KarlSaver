using KVLib;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace KarlSaver
{
    public static class SteamSaveManager
    {
        public static string GetSteamPath()
        {
            var steamRegistryPaths = new[] {
                "HKEY_CURRENT_USER\\Software\\Valve\\Steam",
                "HKEY_CURRENT_USER\\Software\\Wow6432Node\\Valve\\Steam",
                "HKEY_LOCAL_MACHINE\\SOFTWARE\\Valve\\Steam",
                "HKEY_LOCAL_MACHINE\\SOFTWARE\\Wow6432Node\\Valve\\Steam"
            };

            var registryEntryPossibleNames = new[]
            {
                "SteamPath",
                "InstallPath"
            };

            string steamPath = null;
            foreach (var steamRegistryPath in steamRegistryPaths)
            {
                foreach (var entryName in registryEntryPossibleNames)
                {
                    steamPath = (string)Microsoft.Win32.Registry.GetValue(@"HKEY_CURRENT_USER\Software\Valve\Steam", "SteamPath", null);
                    if (!String.IsNullOrEmpty(steamPath))
                        break;
                }

                if (!String.IsNullOrEmpty(steamPath))
                    break;
            }

            return steamPath;
        }

        public static string GetSaveFolderPath()
        {
            var steamInstallPath = GetSteamPath();

            var libraryVdfPath = Path.Combine(steamInstallPath, "steamapps", "libraryfolders.vdf");
            var vdfFileContent = File.ReadAllText(libraryVdfPath);

            var result = KVParser.ParseKeyValueText(vdfFileContent);

            foreach (var path in result.Children)
            {
                var libraryPath = path.Children.ElementAt(0).GetString();

                var deepRockLibraryPath = Path.Combine(libraryPath, "steamapps", "common", "Deep Rock Galactic");
                if (Directory.Exists(deepRockLibraryPath))
                {
                    var fullPath = Path.Combine(deepRockLibraryPath, "FSD", "Saved", "SaveGames");
                    if (Directory.Exists(fullPath))
                        return fullPath;
                }
            }

            return null;
        }

        public static string GetSaveFilePath(string saveFolderPath)
        {
            string[] steamSaveFolderFiles = Directory.GetFiles(saveFolderPath);

            foreach (var file in steamSaveFolderFiles)
            {
                string[] splitString = file.Split('_');

                if (splitString[splitString.Length - 1] == "Player.sav")
                {
                    return file;
                }
            }

            return null;
        }
    }
}
