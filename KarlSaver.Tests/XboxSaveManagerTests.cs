using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarlSaver.Tests
{
    
    public class XboxSaveManagerTests
    {
        [SetUp]
        public void Setup()
        {
        }
        
        [Test]
        public void GetSteamSavePath_Success()
        {
            var response = XboxSaveManager.GetSaveFolderPath();

            Assert.IsNotNull(response);
            Assert.IsNotEmpty(response);
        }

        [Test]
        public void GetSaveFilePath_Success()
        {
            var savePath = XboxSaveManager.GetSaveFolderPath();
            var response = XboxSaveManager.GetSaveFilePath(savePath);

            Assert.IsNotNull(response);
            Assert.IsNotEmpty(response);
        }
    }
}
