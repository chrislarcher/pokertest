using static PokerTest.Enums.CardTypes;

namespace PokerTest.Interfaces
{
    public interface ICardTypeService
    {
        Ranks GetFaceCard(string value);
    }
}
