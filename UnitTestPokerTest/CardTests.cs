using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerTest.Tests
{
    [TestClass()]
    public class CardTests
    {
        [TestMethod()]
        public void ValidCardTest()
        {
            Card card = new Card("2d");
            Assert.AreEqual(card.GetRank(), CardTypes.Ranks.two);
            Assert.AreEqual(card.GetSuit(), CardTypes.Suits.diamonds);
        }

        [TestMethod()]
        public void CardValueTooShortTest()
        {
            try
            {
                Card card = new Card("d");
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
                Card card = new Card("22dd");
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
                Card card = new Card("2l");
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
                Card card = new Card("15d");
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