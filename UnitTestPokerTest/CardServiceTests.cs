using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerGame.Models;
using PokerGame.Models.Enums;
using PokerGame.Services;
using PokerGame.Services.Interfaces;
using System;

namespace PokerTest.Tests
{
    [TestClass()]
    public class CardServiceTests
    {
        ICardTypeService _cardTypeService;
        ICardService _cardService;

        public CardServiceTests()
        {
            _cardTypeService = new CardTypeService();
            _cardService = new CardService(_cardTypeService);
        }

        [TestMethod()]
        public void ValidCardTest()
        {
            Card card = _cardService.GetCard("2d");
            Assert.AreEqual(card.Rank, CardTypes.Ranks.two);
            Assert.AreEqual(card.Suit, CardTypes.Suits.diamonds);
        }

        [TestMethod()]
        public void CardValueTooShortTest()
        {
            try
            {
                Card card = _cardService.GetCard("d");
            }
            catch(FormatException e)
            {
                StringAssert.Contains(e.Message, "Invalid card");
                return;
            }
            
            Assert.Fail("Exception was not thrown");
        }

        [TestMethod()]
        public void CardValueTooLongTest()
        {
            try
            {
                Card card = _cardService.GetCard("22dd");
            }
            catch (FormatException e)
            {
                StringAssert.Contains(e.Message, "Invalid card");
                return;
            }

            Assert.Fail("Exception was not thrown");
        }

        [TestMethod()]
        public void InvalidSuitTest()
        {
            try
            {
                Card card = _cardService.GetCard("2l");
            }
            catch (FormatException e)
            {
                StringAssert.Contains(e.Message, "Invalid suit");
                return;
            }

            Assert.Fail("Exception was not thrown");
        }

        [TestMethod()]
        public void InvalidRankTest()
        {
            try
            {
                Card card = _cardService.GetCard("15d");
            }
            catch (FormatException e)
            {
                StringAssert.Contains(e.Message, "Invalid rank");
                return;
            }

            Assert.Fail("Exception was not thrown");
        }
    }
}