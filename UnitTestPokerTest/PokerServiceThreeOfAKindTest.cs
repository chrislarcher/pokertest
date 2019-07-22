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
        ICardTypeService cardTypeService;
        ICardService cardService;
        IPokerHandService pokerHandService;
        IPlayerService playerService;
        IPlayerImportService playerImportService;
        IPokerService pokerService;
        IHighestCardService highestCardService;

        public PokerServiceThreeOfAKindTest()
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
            players.Add(playerService.GetPlayer("Joe, 8D, 6S, AC, QD, JD"));
            players.Add(playerService.GetPlayer("Bob, AD, AS, AC, 6S, 4S"));
            players.Add(playerService.GetPlayer("Sally, 4H, 4S, AH, QH, 8H"));

            Assert.AreEqual(pokerService.GetWinnerName(players), "Bob");
        }

        [TestMethod()]
        public void MultipleHighestTest()
        {
            List<Player> players = new List<Player>();
            players.Add(playerService.GetPlayer("Joe, 8D, 6S, 6C, 6D, JD"));
            players.Add(playerService.GetPlayer("Bob, QS, 10S, QS, 6D, 4C"));
            players.Add(playerService.GetPlayer("Sally, 9H, 9H, 9C, QH, 8H"));

            Assert.AreEqual(pokerService.GetWinnerName(players), "Sally");
        }

        [TestMethod()]
        public void MultipleFourTest()
        {
            List<Player> players = new List<Player>();
            players.Add(playerService.GetPlayer("Joe, 8D, 9S, 9C, 9D, JD"));
            players.Add(playerService.GetPlayer("Bob, QS, 10S, QS, 6D, 4C"));
            players.Add(playerService.GetPlayer("Sally, 9H, 9H, 9C, QH, 8H"));

            Assert.AreEqual(pokerService.GetWinnerName(players), "Sally");
        }


        [TestMethod()]
        public void MultipleFifthTest()
        {
            List<Player> players = new List<Player>();
            players.Add(playerService.GetPlayer("Joe, QD, 9S, 9C, 9D, JD"));
            players.Add(playerService.GetPlayer("Bob, QS, 10S, QS, 6D, 4C"));
            players.Add(playerService.GetPlayer("Sally, 9H, 9H, 9C, 8H, JH"));

            Assert.AreEqual(pokerService.GetWinnerName(players), "Joe");
        }

        [TestMethod()]
        public void TwoWinnersTest()
        {
            List<Player> players = new List<Player>();
            players.Add(playerService.GetPlayer("Joe, 9H, 9H, 9C, QH, 8H"));
            players.Add(playerService.GetPlayer("Bob, QS, 10S, QS, 6D, 4C"));
            players.Add(playerService.GetPlayer("Sally, 9H, 9H, 9C, QH, 8H"));

            Assert.AreEqual(pokerService.GetWinnerName(players), "Joe, Sally");
        }

        [TestMethod()]
        public void ThreeWinnersTest()
        {
            List<Player> players = new List<Player>();
            players.Add(playerService.GetPlayer("Joe, 9H, 9H, 9C, QH, 8H"));
            players.Add(playerService.GetPlayer("Bob, 9H, 9H, 9C, QH, 8H"));
            players.Add(playerService.GetPlayer("Sally, 9H, 9H, 9C, QH, 8H"));

            Assert.AreEqual(pokerService.GetWinnerName(players), "Joe, Bob, Sally");
        }
    }
}