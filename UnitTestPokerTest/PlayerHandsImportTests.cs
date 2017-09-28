using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PokerTest.Tests
{
    [TestClass()]
    public class PlayerHandsImportTests
    {
        [TestMethod()]
        public void FileImportSuccessTest()
        {
            PlayerHandsImport playerHandsImport = new PlayerHandsImport("..\\..\\..\\UnitTestPokerTest\\ValidPlayerHand.txt");
            List<PlayersHand> importedHands = playerHandsImport.GetPlayerHands();

            PlayersHand importedHand = importedHands.First();
            PlayersHand testHands = new PlayersHand("Billy, 2d, 3h, qh, kh, ah");

            Assert.AreEqual(importedHand.GetName(), testHands.GetName());
            Assert.AreEqual(importedHand.GetHighestCard(), testHands.GetHighestCard());
        }

        [TestMethod()]
        public void NoFileTest()
        {
            try
            {
                PlayerHandsImport playerHandsImport = new PlayerHandsImport("test.txt");
            }
            catch (FileNotFoundException e)
            {
                StringAssert.Contains(e.Message, "No Player Info File Found.  Exiting program...");
                return;
            }

            Assert.Fail("Exception was not thrown");
        }
    }
}