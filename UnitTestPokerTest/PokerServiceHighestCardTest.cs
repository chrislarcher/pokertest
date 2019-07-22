using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerTest.Interfaces;
using PokerTest.Models;
using PokerTest.Services;
using System.Collections.Generic;

namespace PokerTest.Tests
{
    [TestClass()]
    public class PokerServiceHighestCardTest
    {
        ICardTypeService cardTypeService;
        ICardService cardService;
        IPokerHandService pokerHandService;
        IPlayerService playerService;
        IPlayerImportService playerImportService;
        IPokerService pokerService;

        public PokerServiceHighestCardTest()
        {
            cardTypeService = new CardTypeService();
            cardService = new CardService(cardTypeService);
            pokerHandService = new PokerHandService(cardService);
            playerService = new PlayerService(cardService, pokerHandService);
            playerImportService = new PlayerImportService(playerService);
            pokerService = new PokerService(pokerHandService, playerService, cardService);
        }

        [TestMethod()]
        public void SingleWinnerTest()
        {
            List<Player> players = new List<Player>();
            players.Add(playerService.GetPlayer("Joe, 8S, 6D, AH, QD, JD"));
            players.Add(playerService.GetPlayer("Bob, 3D, 4S, 5C, 6H, 7S"));
            players.Add(playerService.GetPlayer("Sally, 4H, 2S, 3H, QC, 8H"));

            Assert.AreEqual(pokerService.GetWinnerName(players), "Joe");
        }

        [TestMethod()]
        public void SecondHighestTest()
        {
            List<Player> players = new List<Player>();
            players.Add(playerService.GetPlayer("Joe, 8S, 6D, AH, QD, JD"));
            players.Add(playerService.GetPlayer("Bob, 3D, 4S, 5C, 6H, 7S"));
            players.Add(playerService.GetPlayer("Sally, 4H, AS, 3H, KC, 8H"));

            Assert.AreEqual(pokerService.GetWinnerName(players), "Sally");
        }

        [TestMethod()]
        public void ThirdHighestTest()
        {
            List<Player> players = new List<Player>();
            players.Add(playerService.GetPlayer("Joe, 8S, 6D, AH, QD, JD"));
            players.Add(playerService.GetPlayer("Bob, 3D, 4S, AC, QH, KS"));
            players.Add(playerService.GetPlayer("Sally, 4H, 2S, 3H, QC, 8H"));

            Assert.AreEqual(pokerService.GetWinnerName(players), "Bob");
        }

        [TestMethod()]
        public void FourHighestTest()
        {
            List<Player> players = new List<Player>();
            players.Add(playerService.GetPlayer("Joe, 8S, KD, AH, QD, JD"));
            players.Add(playerService.GetPlayer("Bob, 3D, AS, QC, KH, 7S"));
            players.Add(playerService.GetPlayer("Sally, 4H, 2S, 3H, QC, 8H"));

            Assert.AreEqual(pokerService.GetWinnerName(players), "Joe");
        }


        [TestMethod()]
        public void FifthHighestTest()
        {
            List<Player> players = new List<Player>();
            players.Add(playerService.GetPlayer("Joe, QS, AD, 4H, KD, JD"));
            players.Add(playerService.GetPlayer("Bob, 3D, 4S, 5C, 6H, 7S"));
            players.Add(playerService.GetPlayer("Sally, AH, QS, JH, KC, 5H"));

            Assert.AreEqual(pokerService.GetWinnerName(players), "Sally");
        }

        [TestMethod()]
        public void TwoWinnersTest()
        {
            List<Player> players = new List<Player>();
            players.Add(playerService.GetPlayer("Joe, 8S, KD, AH, QD, JD"));
            players.Add(playerService.GetPlayer("Bob, 8S, KD, AH, QD, JD"));
            players.Add(playerService.GetPlayer("Sally, 4H, 2S, 3H, QC, 8H"));

            Assert.AreEqual(pokerService.GetWinnerName(players), "Joe, Bob");
        }

        [TestMethod()]
        public void ThreeWinnersTest()
        {
            List<Player> players = new List<Player>();
            players.Add(playerService.GetPlayer("Joe, 8S, KD, AH, QD, JD"));
            players.Add(playerService.GetPlayer("Bob, 8S, KD, AH, QD, JD"));
            players.Add(playerService.GetPlayer("Sally, 8S, KD, AH, QD, JD"));

            Assert.AreEqual(pokerService.GetWinnerName(players), "Joe, Bob, Sally");
        }
    }
}