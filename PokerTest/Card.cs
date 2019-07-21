using System;
using static PokerTest.CardTypes;

namespace PokerTest
{
    public class Card
    {
        private Ranks _rank;
        private Suits _suit;

        public Card(string card)
        {
            //assuming if invalid card is found, game should terminate.
            if (card.Length < 2 || card.Length > 3)
            {
                throw new FormatException("Invalid card");
            }

            SetSuit(card);
            SetRank(card);           
        }

        public Suits GetSuit()
        {
            return _suit;
        }

        public Ranks GetRank()
        {
            return _rank;
        }

        private void SetSuit(string card)
        {
            char[] suitIdentifier = card.ToCharArray();
            Int32 enumValue = char.ToLower(suitIdentifier[card.Length - 1]);

            if (Enum.IsDefined(typeof(Suits), enumValue))
            {
                _suit = (Suits)enumValue;
            }
            else
            {
                throw new FormatException("Invalid suit");
            }
        }

        private void SetRank(string card)
        {
            string value = card.Substring(0, card.Length - 1);

            if (int.TryParse(value, out int number))
            {
                if (Enum.IsDefined(typeof(Ranks), number))
                {
                    _rank = (Ranks)number;
                }
                else
                {
                    throw new FormatException("Invalid rank");
                }
            }
            else
            {
                _rank = GetFaceCard(value);
            }
        }
    }
}
