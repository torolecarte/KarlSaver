using NUnit.Framework;

namespace KarlSaver.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GetSteamPath_Success()
        {
            var response = SteamSaveManager.GetSteamPath();

            Assert.IsNotNull(response);
            Assert.IsNotEmpty(response);
        }

        [Test]
        public void GetSteamSavePath_Success()
        {
            var response = SteamSaveManager.GetSteamSavePath();

            Assert.IsNotNull(response);
            Assert.IsNotEmpty(response);
        }

        [Test]
        public void GetDeepRockLibraryFolderPath_Success()
        {
            var steamPath = SteamSaveManager.GetSteamPath();
            var response = SteamSaveManager.GetDeepRockSaveFolderPath(steamPath);

            Assert.IsNotNull(response);
            Assert.IsNotEmpty(response);
        }
    }
}