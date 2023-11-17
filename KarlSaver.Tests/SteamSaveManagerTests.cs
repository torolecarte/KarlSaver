using NUnit.Framework;

namespace KarlSaver.Tests
{
    public class SteamSaveManagerTests
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
            var response = SteamSaveManager.GetSaveFolderPath();

            Assert.IsNotNull(response);
            Assert.IsNotEmpty(response);
        }

        [Test]
        public void GetSaveFilePath_Success()
        {
            var steamPath = SteamSaveManager.GetSteamPath();
            var response = SteamSaveManager.GetSaveFilePath(steamPath);

            Assert.IsNotNull(response);
            Assert.IsNotEmpty(response);
        }
    }
}