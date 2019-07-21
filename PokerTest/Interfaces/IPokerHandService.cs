using PokerTest.Models;
using System.Collections.Generic;
using static PokerTest.Enums.PokerHands;

namespace PokerTest.Interfaces
{
    public interface IPokerHandService
    {
        PokerHand GetPokerHand(List<Card> cards);
    }
}
