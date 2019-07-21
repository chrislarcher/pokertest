using System;
using PokerTest.Models;
using PokerTest.Interfaces;
using System.Collections.Generic;
using static PokerTest.Enums.CardTypes;

namespace PokerTest.Services
{
    public class CardService : ICardService
    {
        private ICardTypeService _cardTypeService;
        private Card _card;

        public CardService(ICardTypeService cardTypeService)
        {
            _cardTypeService = cardTypeService;
        }

        public Card GetCard(string card)
        {
            _card = new Card();

            //assuming if invalid card is found, game should terminate.
            if (card.Length < 2 || card.Length > 3)
            {
                throw new FormatException("Invalid card");
            }

            SetSuit(card);
            SetRank(card);

            return _card;
        }

        public int GetHighestCard(List<Card> cards)
        {
            int HighestCard = 0;

            foreach (Card card in cards)
            {
                if ((int)card.Rank > HighestCard)
                {
                    HighestCard = (int)card.Rank;
                }
            }

            return HighestCard;
        }

        private void SetSuit(string card)
        {
            char[] suitIdentifier = card.ToCharArray();
            Int32 enumValue = char.ToLower(suitIdentifier[card.Length - 1]);

            if (Enum.IsDefined(typeof(Suits), enumValue))
            {
                _card.Suit = (Suits)enumValue;
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
                    _card.Rank = (Ranks)number;
                }
                else
                {
                    throw new FormatException("Invalid rank");
                }
            }
            else
            {
                _card.Rank = _cardTypeService.GetFaceCard(value);
            }
        }
    }
}
