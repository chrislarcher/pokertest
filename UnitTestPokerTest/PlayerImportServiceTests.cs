using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerGame.Models;
using PokerGame.Services;
using PokerGame.Services.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PokerTest.Tests
{
    [TestClass()]
    public class PlayerImportServiceTest
    {
        ICardTypeService _cardTypeService;
        ICardService _cardService;
        IPokerHandService _pokerHandService;
        IPlayerService _playerService;
        IPlayerImportService _playerImportService;

        public PlayerImportServiceTest()
        {
            _cardTypeService = new CardTypeService();
            _cardService = new CardService(_cardTypeService);
            _pokerHandService = new PokerHandService(_cardService);
            _playerService = new PlayerService(_cardService, _pokerHandService);
            _playerImportService = new PlayerImportService(_playerService);
        }

        [TestMethod()]
        public void FileImportSuccessTest()
        {
            List<Player> players = _playerImportService.GetPlayers("..\\..\\..\\UnitTestPokerTest\\ValidPlayerHand.txt");

            Player actual = players.FirstOrDefault();
            Player expected = _playerService.GetPlayer("Billy, 2d, 3h, qh, kh, ah");

            Card actualCard = actual.Hand.Cards.FirstOrDefault();
            Card expectedCard = expected.Hand.Cards.FirstOrDefault();

            Assert.AreEqual(actual.Name, expected.Name);
            Assert.AreEqual(actualCard.Rank, expectedCard.Rank);
            Assert.AreEqual(actualCard.Suit, expectedCard.Suit);
        }

        [TestMethod()]
        public void NoFileTest()
        {
            try
            {
                List<Player> players = _playerImportService.GetPlayers("test.txt");
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