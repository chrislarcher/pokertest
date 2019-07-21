using PokerTest.Models;

namespace PokerTest.Interfaces
{
    public interface IPlayerService
    {
        Player GetPlayer(string playerInfo);
    }
}
