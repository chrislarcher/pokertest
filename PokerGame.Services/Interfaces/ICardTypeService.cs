
using static PokerGame.Models.Enums.CardTypes;

namespace PokerGame.Services.Interfaces
{
    public interface ICardTypeService
    {
        Ranks GetFaceCard(string value);
    }
}
