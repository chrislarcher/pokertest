using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerGame.Models;
using PokerGame.Services;
using PokerGame.Services.Interfaces;
using PokerTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerTest.Tests
{
    [TestClass()]
    public class PlayerServiceTest
    {
        ICardTypeService _cardTypeService;
        ICardService _cardService;
        IPokerHandService _pokerHandService;
        IPlayerService _playerService;

        public PlayerServiceTest()
        {
            _cardTypeService = new CardTypeService();
            _cardService = new CardService(_cardTypeService);
            _pokerHandService = new PokerHandService(_cardService);
            _playerService = new PlayerService(_cardService, _pokerHandService);
        }

        [TestMethod()]
        public void IncorrectNumberOfParametersTest()
        {
            try
            {
                Player player = _playerService.GetPlayer("test, d3");
            }
            catch (FormatException e)
            {
                StringAssert.Contains(e.Message, "Incorrect number of parameters.");
                return;
            }

            Assert.Fail("Exception was not thrown");
        }

        [TestMethod()]
        public void GetNameTest()
        {
            Player player = _playerService.GetPlayer("Billy, 2d, 3h, qh, kh, ah");
            Assert.AreEqual(player.Name, "Billy");
        }
    }
}