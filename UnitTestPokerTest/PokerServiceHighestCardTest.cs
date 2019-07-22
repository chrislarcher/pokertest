using PokerGame.Models;
using PokerGame.Services;
using System.Collections.Generic;
using PokerGame.Services.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PokerTest.Tests
{
    [TestClass()]
    public class PokerServiceHighestCardTest
    {
        ICardTypeService _cardTypeService;
        ICardService _cardService;
        IPokerHandService _pokerHandService;
        IPlayerService _playerService;
        IPlayerImportService _playerImportService;
        IPokerService _pokerService;
        IHighestCardService _highestCardService;

        public PokerServiceHighestCardTest()
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
            players.Add(_playerService.GetPlayer("Joe, 8S, 6D, AH, QD, JD"));
            players.Add(_playerService.GetPlayer("Bob, 3D, 4S, 5C, 6H, 7S"));
            players.Add(_playerService.GetPlayer("Sally, 4H, 2S, 3H, QC, 8H"));

            Assert.AreEqual(_pokerService.GetWinnerName(players), "Joe");
        }

        [TestMethod()]
        public void SecondHighestTest()
        {
            List<Player> players = new List<Player>();
            players.Add(_playerService.GetPlayer("Joe, 8S, 6D, AH, QD, JD"));
            players.Add(_playerService.GetPlayer("Bob, 3D, 4S, 5C, 6H, 7S"));
            players.Add(_playerService.GetPlayer("Sally, 4H, AS, 3H, KC, 8H"));

            Assert.AreEqual(_pokerService.GetWinnerName(players), "Sally");
        }

        [TestMethod()]
        public void ThirdHighestTest()
        {
            List<Player> players = new List<Player>();
            players.Add(_playerService.GetPlayer("Joe, 8S, 6D, AH, QD, JD"));
            players.Add(_playerService.GetPlayer("Bob, 3D, 4S, AC, QH, KS"));
            players.Add(_playerService.GetPlayer("Sally, 4H, 2S, 3H, QC, 8H"));

            Assert.AreEqual(_pokerService.GetWinnerName(players), "Bob");
        }

        [TestMethod()]
        public void FourHighestTest()
        {
            List<Player> players = new List<Player>();
            players.Add(_playerService.GetPlayer("Joe, 8S, KD, AH, QD, JD"));
            players.Add(_playerService.GetPlayer("Bob, 3D, AS, QC, KH, 7S"));
            players.Add(_playerService.GetPlayer("Sally, 4H, 2S, 3H, QC, 8H"));

            Assert.AreEqual(_pokerService.GetWinnerName(players), "Joe");
        }


        [TestMethod()]
        public void FifthHighestTest()
        {
            List<Player> players = new List<Player>();
            players.Add(_playerService.GetPlayer("Joe, QS, AD, 4H, KD, JD"));
            players.Add(_playerService.GetPlayer("Bob, 3D, 4S, 5C, 6H, 7S"));
            players.Add(_playerService.GetPlayer("Sally, AH, QS, JH, KC, 5H"));

            Assert.AreEqual(_pokerService.GetWinnerName(players), "Sally");
        }

        [TestMethod()]
        public void TwoWinnersTest()
        {
            List<Player> players = new List<Player>();
            players.Add(_playerService.GetPlayer("Joe, 8S, KD, AH, QD, JD"));
            players.Add(_playerService.GetPlayer("Bob, 8S, KD, AH, QD, JD"));
            players.Add(_playerService.GetPlayer("Sally, 4H, 2S, 3H, QC, 8H"));

            Assert.AreEqual(_pokerService.GetWinnerName(players), "Joe, Bob");
        }

        [TestMethod()]
        public void ThreeWinnersTest()
        {
            List<Player> players = new List<Player>();
            players.Add(_playerService.GetPlayer("Joe, 8S, KD, AH, QD, JD"));
            players.Add(_playerService.GetPlayer("Bob, 8S, KD, AH, QD, JD"));
            players.Add(_playerService.GetPlayer("Sally, 8S, KD, AH, QD, JD"));

            Assert.AreEqual(_pokerService.GetWinnerName(players), "Joe, Bob, Sally");
        }
    }
}