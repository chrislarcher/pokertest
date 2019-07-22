using PokerGame.Models;
using System.Collections.Generic;

namespace PokerGame.Services.Interfaces
{
    public interface IPlayerImportService
    {
        List<Player> GetPlayers(string filePath);
    }
}
