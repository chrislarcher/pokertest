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
        ICardTypeService cardTypeService;
        ICardService cardService;
        IPokerHandService pokerHandService;
        IPlayerService playerService;
        IPlayerImportService playerImportService;
        IPokerService pokerService;
        IHighestCardService highestCardService;

        public PokerServicePairTest()
        {
            cardTypeService = new CardTypeService();
            cardService = new CardService(cardTypeService);
            pokerHandService = new PokerHandService(cardService);
            playerService = new PlayerService(cardService, pokerHandService);
            playerImportService = new PlayerImportService(playerService);
            highestCardService = new HighestCardService();
            pokerService = new PokerService(highestCardService, playerService, cardService);
        }

        [TestMethod()]
        public void SingleWinnerTest()
        {
            List<Player> players = new List<Player>();
            players.Add(playerService.GetPlayer("Joe, 8D, 6H, AD, QC, JS"));
            players.Add(playerService.GetPlayer("Bob, AD, AS, KC, 6S, 4H"));
            players.Add(playerService.GetPlayer("Sally, 4C, 6S, AH, QH, 8D"));

            Assert.AreEqual(pokerService.GetWinnerName(players), "Bob");
        }

        [TestMethod()]
        public void MultipleHighestTest()
        {
            List<Player> players = new List<Player>();
            players.Add(playerService.GetPlayer("Joe, 8D, 8H, AD, QC, JS"));
            players.Add(playerService.GetPlayer("Bob, AD, AS, KC, 6S, 4H"));
            players.Add(playerService.GetPlayer("Sally, 4C, 6S, AH, QH, 8D"));

            Assert.AreEqual(pokerService.GetWinnerName(players), "Bob");
        }

        [TestMethod()]
        public void MultipleThirdTest()
        {
            List<Player> players = new List<Player>();
            players.Add(playerService.GetPlayer("Joe, AD, AH, JD, QC, JS"));
            players.Add(playerService.GetPlayer("Bob, AD, AS, KC, 6S, 4H"));
            players.Add(playerService.GetPlayer("Sally, 4C, 6S, AH, QH, 8D"));

            Assert.AreEqual(pokerService.GetWinnerName(players), "Bob");
        }

        [TestMethod()]
        public void MultipleFourTest()
        {
            List<Player> players = new List<Player>();
            players.Add(playerService.GetPlayer("Joe, AD, AH, JD, QC, 10S"));
            players.Add(playerService.GetPlayer("Bob, AD, AS, JC, 6S, 4H"));
            players.Add(playerService.GetPlayer("Sally, 4C, 6S, AH, QH, 8D"));

            Assert.AreEqual(pokerService.GetWinnerName(players), "Joe");
        }


        [TestMethod()]
        public void MultipleFifthTest()
        {
            List<Player> players = new List<Player>();
            players.Add(playerService.GetPlayer("Joe, AD, AH, KD, QC, JS"));
            players.Add(playerService.GetPlayer("Bob, AD, AS, KC, 4S, QH"));
            players.Add(playerService.GetPlayer("Sally, 4C, 6S, AH, QH, 8D"));

            Assert.AreEqual(pokerService.GetWinnerName(players), "Joe");
        }

        [TestMethod()]
        public void TwoWinnersTest()
        {
            List<Player> players = new List<Player>();
            players.Add(playerService.GetPlayer("Joe, AD, AH, KD, QC, JS"));
            players.Add(playerService.GetPlayer("Bob, AD, AH, KD, QC, JS"));
            players.Add(playerService.GetPlayer("Sally, KS, JH, AH, QH, 10H"));

            Assert.AreEqual(pokerService.GetWinnerName(players), "Joe, Bob");
        }

        [TestMethod()]
        public void ThreeWinnersTest()
        {
            List<Player> players = new List<Player>();
            players.Add(playerService.GetPlayer("Joe, AD, AH, KD, QC, JS"));
            players.Add(playerService.GetPlayer("Bob, AD, AH, KD, QC, JS"));
            players.Add(playerService.GetPlayer("Sally, AD, AH, KD, QC, JS"));

            Assert.AreEqual(pokerService.GetWinnerName(players), "Joe, Bob, Sally");
        }
    }
}