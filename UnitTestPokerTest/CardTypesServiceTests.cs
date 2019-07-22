using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerGame.Services;
using PokerGame.Services.Interfaces;
using System;
using static PokerGame.Models.Enums.CardTypes;

namespace PokerTest.Tests
{
    [TestClass()]
    public class CardTypesServiceTests
    {
        ICardTypeService _cardTypeService;

        public CardTypesServiceTests()
        {
            _cardTypeService = new CardTypeService();
        }

        [TestMethod()]
        public void ValidFaceCardsTest()
        {
            Assert.AreEqual(_cardTypeService.GetFaceCard("j"), Ranks.jack);
            Assert.AreEqual(_cardTypeService.GetFaceCard("q"), Ranks.queen);
            Assert.AreEqual(_cardTypeService.GetFaceCard("k"), Ranks.king);
            Assert.AreEqual(_cardTypeService.GetFaceCard("a"), Ranks.ace);
        }

        [TestMethod()]
        public void InvalidFaceCardsTest()
        {
            try
            {
                Ranks rank = _cardTypeService.GetFaceCard("l");
            }
            catch (FormatException e)
            {
                StringAssert.Contains(e.Message, "Invalid Face Card");
                return;
            }

            Assert.Fail("Exception was not thrown");
        }
    }
}