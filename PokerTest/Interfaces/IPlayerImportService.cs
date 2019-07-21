using PokerTest.Models;
using System.Collections.Generic;

namespace PokerTest.Interfaces
{
    public interface IPlayerImportService
    {
        List<Player> GetPlayers();
    }
}
