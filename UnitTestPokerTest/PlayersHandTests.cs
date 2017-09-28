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
    public class PlayersHandTests
    {
        [TestMethod()]
        public void IncorrectNumberOfParametersTest()
        {
            try
            {
                PlayersHand playersHand = new PlayersHand("test, d3");
            }
            catch (FormatException e)
            {
                StringAssert.Contains(e.Message, "Incorrect number of parameters.");
                return;
            }

            Assert.Fail("Exception was not thrown");
        }

        [TestMethod()]
        public void GetNameTest()
        {
            PlayersHand playersHand = new PlayersHand("Billy, 2d, 3h, qh, kh, ah");
            Assert.AreEqual(playersHand.GetName(), "Billy");
        }

        [TestMethod()]
        public void GetHandTest()
        {
            PlayersHand playersHand = new PlayersHand("Billy, 2h, 3h, qh, kh, ah");
            PokerHands pokerHands = new PokerHands();
            List<Card> Cards = new List<Card>();
            Cards.Add(new Card("2h"));
            Cards.Add(new Card("3h"));
            Cards.Add(new Card("qh"));
            Cards.Add(new Card("kh"));
            Cards.Add(new Card("ah"));

            Assert.AreEqual(playersHand.GetHand(), pokerHands.GetPokerHand(Cards));
        }

        [TestMethod()]
        public void GetHighestCardTest()
        {
            PlayersHand playersHand = new PlayersHand("Billy, 2d, 3h, qh, kh, ah");

            Assert.AreEqual(playersHand.GetHighestCard(), (int)new Card("ah").GetRank());
        }
    }
}