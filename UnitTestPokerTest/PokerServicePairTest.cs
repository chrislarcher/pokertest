using PokerGame.Models;
using PokerGame.Services;
using System.Collections.Generic;
using PokerGame.Services.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PokerTest.Tests
{
    [TestClass()]
    public class PokerServicePairTest
    {
        ICardTypeService _cardTypeService;
        ICardService _cardService;
        IPokerHandService _pokerHandService;
        IPlayerService _playerService;
        IPlayerImportService _playerImportService;
        IPokerService _pokerService;
        IHighestCardService _highestCardService;

        public PokerServicePairTest()
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
            players.Add(_playerService.GetPlayer("Joe, 8D, 6H, AD, QC, JS"));
            players.Add(_playerService.GetPlayer("Bob, AD, AS, KC, 6S, 4H"));
            players.Add(_playerService.GetPlayer("Sally, 4C, 6S, AH, QH, 8D"));

            Assert.AreEqual(_pokerService.GetWinnerName(players), "Bob");
        }

        [TestMethod()]
        public void MultipleHighestTest()
        {
            List<Player> players = new List<Player>();
            players.Add(_playerService.GetPlayer("Joe, 8D, 8H, AD, QC, JS"));
            players.Add(_playerService.GetPlayer("Bob, AD, AS, KC, 6S, 4H"));
            players.Add(_playerService.GetPlayer("Sally, 4C, 6S, AH, QH, 8D"));

            Assert.AreEqual(_pokerService.GetWinnerName(players), "Bob");
        }

        [TestMethod()]
        public void MultipleThirdTest()
        {
            List<Player> players = new List<Player>();
            players.Add(_playerService.GetPlayer("Joe, AD, AH, JD, QC, JS"));
            players.Add(_playerService.GetPlayer("Bob, AD, AS, KC, 6S, 4H"));
            players.Add(_playerService.GetPlayer("Sally, 4C, 6S, AH, QH, 8D"));

            Assert.AreEqual(_pokerService.GetWinnerName(players), "Bob");
        }

        [TestMethod()]
        public void MultipleFourTest()
        {
            List<Player> players = new List<Player>();
            players.Add(_playerService.GetPlayer("Joe, AD, AH, JD, QC, 10S"));
            players.Add(_playerService.GetPlayer("Bob, AD, AS, JC, 6S, 4H"));
            players.Add(_playerService.GetPlayer("Sally, 4C, 6S, AH, QH, 8D"));

            Assert.AreEqual(_pokerService.GetWinnerName(players), "Joe");
        }


        [TestMethod()]
        public void MultipleFifthTest()
        {
            List<Player> players = new List<Player>();
            players.Add(_playerService.GetPlayer("Joe, AD, AH, KD, QC, JS"));
            players.Add(_playerService.GetPlayer("Bob, AD, AS, KC, 4S, QH"));
            players.Add(_playerService.GetPlayer("Sally, 4C, 6S, AH, QH, 8D"));

            Assert.AreEqual(_pokerService.GetWinnerName(players), "Joe");
        }

        [TestMethod()]
        public void TwoWinnersTest()
        {
            List<Player> players = new List<Player>();
            players.Add(_playerService.GetPlayer("Joe, AD, AH, KD, QC, JS"));
            players.Add(_playerService.GetPlayer("Bob, AD, AH, KD, QC, JS"));
            players.Add(_playerService.GetPlayer("Sally, KS, JH, AH, QH, 10H"));

            Assert.AreEqual(_pokerService.GetWinnerName(players), "Joe, Bob");
        }

        [TestMethod()]
        public void ThreeWinnersTest()
        {
            List<Player> players = new List<Player>();
            players.Add(_playerService.GetPlayer("Joe, AD, AH, KD, QC, JS"));
            players.Add(_playerService.GetPlayer("Bob, AD, AH, KD, QC, JS"));
            players.Add(_playerService.GetPlayer("Sally, AD, AH, KD, QC, JS"));

            Assert.AreEqual(_pokerService.GetWinnerName(players), "Joe, Bob, Sally");
        }
    }
}