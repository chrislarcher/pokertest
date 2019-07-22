using PokerGame.Models;
using PokerGame.Services;
using System.Collections.Generic;
using PokerGame.Services.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PokerTest.Tests
{
    [TestClass()]
    public class PokerServiceThreeOfAKindTest
    {
        ICardTypeService _cardTypeService;
        ICardService _cardService;
        IPokerHandService _pokerHandService;
        IPlayerService _playerService;
        IPlayerImportService _playerImportService;
        IPokerService _pokerService;
        IHighestCardService _highestCardService;

        public PokerServiceThreeOfAKindTest()
        {
            _cardTypeService = new CardTypeService();
            _cardService = new CardService(_cardTypeService);
            _pokerHandService = new PokerHandService(_cardService);
            _playerService = new PlayerService(_cardService, _pokerHandService);
            _playerImportService = new PlayerImportService(_playerService);
            _highestCardService = new HighestCardService();
            _pokerService = new PokerService(_highestCardService, _playerService, _cardService);
        }

        [TestMethod()]
        public void SingleWinnerTest()
        {
            List<Player> players = new List<Player>();
            players.Add(_playerService.GetPlayer("Joe, 8D, 6S, AC, QD, JD"));
            players.Add(_playerService.GetPlayer("Bob, AD, AS, AC, 6S, 4S"));
            players.Add(_playerService.GetPlayer("Sally, 4H, 4S, AH, QH, 8H"));

            Assert.AreEqual(_pokerService.GetWinnerName(players), "Bob");
        }

        [TestMethod()]
        public void MultipleHighestTest()
        {
            List<Player> players = new List<Player>();
            players.Add(_playerService.GetPlayer("Joe, 8D, 6S, 6C, 6D, JD"));
            players.Add(_playerService.GetPlayer("Bob, QS, 10S, QS, 6D, 4C"));
            players.Add(_playerService.GetPlayer("Sally, 9H, 9H, 9C, QH, 8H"));

            Assert.AreEqual(_pokerService.GetWinnerName(players), "Sally");
        }

        [TestMethod()]
        public void MultipleFourTest()
        {
            List<Player> players = new List<Player>();
            players.Add(_playerService.GetPlayer("Joe, 8D, 9S, 9C, 9D, JD"));
            players.Add(_playerService.GetPlayer("Bob, QS, 10S, QS, 6D, 4C"));
            players.Add(_playerService.GetPlayer("Sally, 9H, 9H, 9C, QH, 8H"));

            Assert.AreEqual(_pokerService.GetWinnerName(players), "Sally");
        }


        [TestMethod()]
        public void MultipleFifthTest()
        {
            List<Player> players = new List<Player>();
            players.Add(_playerService.GetPlayer("Joe, QD, 9S, 9C, 9D, JD"));
            players.Add(_playerService.GetPlayer("Bob, QS, 10S, QS, 6D, 4C"));
            players.Add(_playerService.GetPlayer("Sally, 9H, 9H, 9C, 8H, JH"));

            Assert.AreEqual(_pokerService.GetWinnerName(players), "Joe");
        }

        [TestMethod()]
        public void TwoWinnersTest()
        {
            List<Player> players = new List<Player>();
            players.Add(_playerService.GetPlayer("Joe, 9H, 9H, 9C, QH, 8H"));
            players.Add(_playerService.GetPlayer("Bob, QS, 10S, QS, 6D, 4C"));
            players.Add(_playerService.GetPlayer("Sally, 9H, 9H, 9C, QH, 8H"));

            Assert.AreEqual(_pokerService.GetWinnerName(players), "Joe, Sally");
        }

        [TestMethod()]
        public void ThreeWinnersTest()
        {
            List<Player> players = new List<Player>();
            players.Add(_playerService.GetPlayer("Joe, 9H, 9H, 9C, QH, 8H"));
            players.Add(_playerService.GetPlayer("Bob, 9H, 9H, 9C, QH, 8H"));
            players.Add(_playerService.GetPlayer("Sally, 9H, 9H, 9C, QH, 8H"));

            Assert.AreEqual(_pokerService.GetWinnerName(players), "Joe, Bob, Sally");
        }
    }
}