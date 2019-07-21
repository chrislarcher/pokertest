using PokerTest.Models;
using System.Collections.Generic;

namespace PokerTest.Interfaces
{
    public interface IPokerService
    {
        string GetWinnerName(List<Player> players);
    }
}
