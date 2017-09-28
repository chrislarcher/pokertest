using System;
using static PokerTest.CardTypes;

namespace PokerTest
{
    class Card
    {
        private Ranks rank;
        private Suits suit;

        public Card(string card)
        {
            //assuming if invalid card is found, game should terminate.
            if (card.Length < 2 || card.Length > 3)
            {
                Console.Write("Invalid card found.  Exiting program...");
                System.Environment.Exit(-1);
            }

            SetSuit(card);
            SetRank(card);           
        }

        public Suits GetSuit()
        {
            return suit;
        }

        public Ranks GetRank()
        {
            return rank;
        }

        private void SetSuit(string card)
        {
            char[] suitIdentifier = card.ToCharArray();
            Int32 enumValue = (Int32)suitIdentifier[card.Length - 1];

            if (Enum.IsDefined(typeof(Suits), enumValue))
            {
                suit = (Suits)enumValue;
            }
        }

        private void SetRank(string card)
        {
            string value = card.Substring(0, card.Length - 1);

            if (int.TryParse(value, out var number))
            {
                if (Enum.IsDefined(typeof(Ranks), number))
                {
                    rank = (Ranks)number;
                }
            }
            else
            {
                rank = GetFaceCard(value);
            }
        }
    }
}
