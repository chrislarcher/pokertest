using PokerGame.Models;
using System.Collections.Generic;

namespace PokerGame.Services.Interfaces
{
    public interface IHighestCardService
    {
        List<Player> PlayerWithHighestPairs(List<Player> players);
        List<Player> PlayerWithHighestCard(List<Player> players);
    }
}
