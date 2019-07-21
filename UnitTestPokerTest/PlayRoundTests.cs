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
    public class PlayRoundTests
    {
        [TestMethod()]
        public void SingleWinnerTest()
        {
            PlayRound play = new PlayRound();
            List<PlayersHand> playersHands = new List<PlayersHand>();

            playersHands.Add(new PlayersHand("Jake, 4s, 4c, 6d, 10c, js"));
            playersHands.Add(new PlayersHand("Billy, 3d, 3h, 3s, 8h, 9h"));
            playersHands.Add(new PlayersHand("John, 2d, 3h, 4s, 5c, as"));

            Assert.AreEqual(play.GetWinnerName(playersHands), "Billy");
        }

        [TestMethod()]
        public void MultipleWinnersTest()
        {
            PlayRound play = new PlayRound();
            List<PlayersHand> playersHands = new List<PlayersHand>();

            playersHands.Add(new PlayersHand("Jake, 4s, 4c, 4d, 10c, js"));
            playersHands.Add(new PlayersHand("Billy, 3d, 3h, 3s, 8h, jh"));
            playersHands.Add(new PlayersHand("John, 2d, 3h, 4s, 5c, as"));

            Assert.AreEqual(play.GetWinnerName(playersHands), "Jake, Billy");
        }
    }
}