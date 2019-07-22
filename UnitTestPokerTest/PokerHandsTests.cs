using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerGame.Models;
using PokerGame.Services;
using PokerGame.Services.Interfaces;
using static PokerGame.Models.Enums.PokerHands;

namespace PokerTest.Tests
{
    [TestClass()]
    public class PokerHandServiceTest
    {
        ICardTypeService _cardTypeService;
        ICardService _cardService;
        IPokerHandService _pokerHandService;
        IPlayerService _playerService;

        public PokerHandServiceTest()
        {
            _cardTypeService = new CardTypeService();
            _cardService = new CardService(_cardTypeService);
            _pokerHandService = new PokerHandService(_cardService);
            _playerService = new PlayerService(_cardService, _pokerHandService);
        }

        [TestMethod()]
        public void GetPokerHandFlushTest()
        {
            Player player = _playerService.GetPlayer("Billy, 2h, 3h, qh, kh, ah");

            Hand hand = _pokerHandService.GetPokerHand(player.Hand);
            
            Assert.AreEqual(hand.PokerHand, PokerHand.Flush);
        }
    }
}