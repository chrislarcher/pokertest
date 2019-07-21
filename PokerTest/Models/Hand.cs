using System.Collections.Generic;
using static PokerTest.Enums.CardTypes;
using static PokerTest.Enums.PokerHands;

namespace PokerTest.Models
{
    public class Hand
    {
        public List<Card> Cards { get; set; }
        public PokerHand PokerHand { get; set; }
        public Ranks DuplicatesRank { get; set; }
    }
}
