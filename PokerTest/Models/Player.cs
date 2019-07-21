using System.Collections.Generic;
using static PokerTest.Enums.PokerHands;

namespace PokerTest.Models
{
    public class Player
    {
        public string Name { get; set; }
        public Hand Hand { get; set; }
    }
}
