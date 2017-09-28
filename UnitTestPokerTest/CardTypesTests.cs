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
    public class CardTypesTests
    {
        [TestMethod()]
        public void ValidFaceCardsTest()
        {
            Assert.AreEqual(CardTypes.GetFaceCard("j"), CardTypes.Ranks.jack);
            Assert.AreEqual(CardTypes.GetFaceCard("q"), CardTypes.Ranks.queen);
            Assert.AreEqual(CardTypes.GetFaceCard("k"), CardTypes.Ranks.king);
            Assert.AreEqual(CardTypes.GetFaceCard("a"), CardTypes.Ranks.ace);
        }

        [TestMethod()]
        public void InvalidFaceCardsTest()
        {
            try
            {
                CardTypes.Ranks rank = CardTypes.GetFaceCard("l");
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