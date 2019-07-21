using PokerTest.Models;
using System.Collections.Generic;

namespace PokerTest.Interfaces
{
    public interface ICardService
    {
        Card GetCard(string card);
        int GetHighestCard(List<Card> cards);
    }
}
