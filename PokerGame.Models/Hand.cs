using System.Collections.Generic;
using static PokerGame.Models.Enums.CardTypes;
using static PokerGame.Models.Enums.PokerHands;

namespace PokerGame.Models
{
    public class Hand
    {
        public List<Card> Cards { get; set; }
        public PokerHand PokerHand { get; set; }
        public Ranks DuplicatesRank { get; set; }
    }
}
