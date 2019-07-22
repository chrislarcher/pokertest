using System;
using PokerGame.Services.Interfaces;
using static PokerGame.Models.Enums.CardTypes;

namespace PokerGame.Services
{
    public class CardTypeService : ICardTypeService
    {
        public Ranks GetFaceCard(string value)
        {
            Ranks rank;

            switch (value.ToLower())
            {
                case "j":
                    rank = Ranks.jack;
                    break;
                case "q":
                    rank = Ranks.queen;
                    break;
                case "k":
                    rank = Ranks.king;
                    break;
                case "a":
                    rank = Ranks.ace;
                    break;
                default:
                    throw new FormatException("Invalid Face Card");
            }

            return rank;
        }
    }
}
