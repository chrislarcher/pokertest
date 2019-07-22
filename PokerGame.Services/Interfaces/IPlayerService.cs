
using PokerGame.Models;

namespace PokerGame.Services.Interfaces
{
    public interface IPlayerService
    {
        Player GetPlayer(string playerInfo);
    }
}
