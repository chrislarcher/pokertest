using PokerGame.Models;
using System.Collections.Generic;

namespace PokerGame.Services.Interfaces
{
    public interface IPokerService
    {
        string GetWinnerName(List<Player> players);
    }
}
