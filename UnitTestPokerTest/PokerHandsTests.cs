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
    public class PokerHandsTests
    {
        [TestMethod()]
        public void GetPokerHandFlushTest()
        {
            PokerHands pokerHands = new PokerHands();
            List<Card> cards = new List<Card>();
            cards.Add(new Card("2d"));
            cards.Add(new Card("3d"));
            cards.Add(new Card("4d"));
            cards.Add(new Card("5d"));
            cards.Add(new Card("6d"));

            pokerHands.GetPokerHand(cards);
            
            Assert.AreEqual(pokerHands.GetPokerHand(cards), PokerHands.PokerHand.Flush);
        }
    }
}