using PokerGame.Models;
using PokerGame.Services;
using System.Collections.Generic;
using PokerGame.Services.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PokerTest.Tests
{
    [TestClass()]
    public class PokerServiceFlushTest
    {
        ICardTypeService _cardTypeService;
        ICardService _cardService;
        IPokerHandService _pokerHandService;
        IPlayerService _playerService;
        IPlayerImportService _playerImportService;
        IPokerService _pokerService;
        IHighestCardService _highestCardService;

        public PokerServiceFlushTest()
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
            players.Add(_playerService.GetPlayer("Joe, 8D, 6D, AD, QD, JD"));
            players.Add(_playerService.GetPlayer("Bob, AD, AS, AC, 6S, 4S"));
            players.Add(_playerService.GetPlayer("Sally, 4H, 4S, AH, QH, 8H"));

            Assert.AreEqual(_pokerService.GetWinnerName(players), "Joe");
        }

        [TestMethod()]
        public void MultipleHighestTest()
        {
            List<Player> players = new List<Player>();
            players.Add(_playerService.GetPlayer("Joe, 8D, 6D, 7D, QD, JD"));
            players.Add(_playerService.GetPlayer("Bob, QS, JS, KS, 6S, 4S"));
            players.Add(_playerService.GetPlayer("Sally, 4H, 6H, 10H, QH, 8H"));

            Assert.AreEqual(_pokerService.GetWinnerName(players), "Bob");
        }

        [TestMethod()]
        public void MultipleSecondTest()
        {
            List<Player> players = new List<Player>();
            players.Add(_playerService.GetPlayer("Joe, 4H, 6H, 3H, 7H, 8H"));
            players.Add(_playerService.GetPlayer("Bob, 6S, JS, 8S, 5S, 4S"));
            players.Add(_playerService.GetPlayer("Sally, 8D, 7D, JD, 10D, 9D"));

            Assert.AreEqual(_pokerService.GetWinnerName(players), "Sally");
        }

        [TestMethod()]
        public void MultipleThirdTest()
        {
            List<Player> players = new List<Player>();
            players.Add(_playerService.GetPlayer("Joe, 7D, 8D, AD, QD, JD"));
            players.Add(_playerService.GetPlayer("Bob, AS, QS, 8S, 6S, 4S"));
            players.Add(_playerService.GetPlayer("Sally, 5H, 4H, AH, QH, 8H"));

            Assert.AreEqual(_pokerService.GetWinnerName(players), "Joe");
        }

        [TestMethod()]
        public void MultipleFourTest()
        {
            List<Player> players = new List<Player>();
            players.Add(_playerService.GetPlayer("Joe, 8D, KD, AD, QD, JD"));
            players.Add(_playerService.GetPlayer("Bob, AS, QS, KS, 6S, 4S"));
            players.Add(_playerService.GetPlayer("Sally, 4H, 5H, AH, QH, KH"));

            Assert.AreEqual(_pokerService.GetWinnerName(players), "Joe");
        }


        [TestMethod()]
        public void MultipleFifthTest()
        {
            List<Player> players = new List<Player>();
            players.Add(_playerService.GetPlayer("Joe, 7D, KD, AD, QD, JD"));
            players.Add(_playerService.GetPlayer("Bob, AS, QS, KS, JS, 10S"));
            players.Add(_playerService.GetPlayer("Sally, KH, JH, AH, QH, 8H"));

            Assert.AreEqual(_pokerService.GetWinnerName(players), "Bob");
        }

        [TestMethod()]
        public void TwoWinnersTest()
        {
            List<Player> players = new List<Player>();
            players.Add(_playerService.GetPlayer("Joe, 7D, KD, AD, QD, JD"));
            players.Add(_playerService.GetPlayer("Bob, AS, QS, KS, JS, 10S"));
            players.Add(_playerService.GetPlayer("Sally, KH, JH, AH, QH, 10H"));

            Assert.AreEqual(_pokerService.GetWinnerName(players), "Bob, Sally");
        }

        [TestMethod()]
        public void ThreeWinnersTest()
        {
            List<Player> players = new List<Player>();
            players.Add(_playerService.GetPlayer("Joe, 7D, KD, AD, QD, JD"));
            players.Add(_playerService.GetPlayer("Bob, 7D, KD, AD, QD, JD"));
            players.Add(_playerService.GetPlayer("Sally, 7D, KD, AD, QD, JD"));

            Assert.AreEqual(_pokerService.GetWinnerName(players), "Joe, Bob, Sally");
        }
    }
}