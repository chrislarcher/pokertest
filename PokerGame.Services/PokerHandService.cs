using System;
using System.Linq;
using PokerGame.Models;
using PokerGame.Services.Interfaces;
using static PokerGame.Models.Enums.CardTypes;
using static PokerGame.Models.Enums.PokerHands;

namespace PokerGame.Services
{
    public class PokerHandService : IPokerHandService
    {
        private ICardService _cardService;

        private Hand _hand;
        private DuplicateCard _duplicateCard;

        public PokerHandService(ICardService cardService)
        {
            _cardService = cardService;
        }

        public Hand GetPokerHand(Hand hand) 
        {
            _hand = hand;
            GetMatchingCards();

            if (IsFlush())
            {
                _hand.PokerHand = PokerHand.Flush;
                return _hand;
            }
            else if(IsThreeOfAKind())
            {
                _hand.PokerHand = PokerHand.ThreeOfAKind;
                _hand.DuplicatesRank = _duplicateCard.Rank;
                return _hand;
            }
            else if (IsPair())
            {
                _hand.PokerHand = PokerHand.Pair;
                _hand.DuplicatesRank = _duplicateCard.Rank;
                return _hand;
            }
            else
            {
                _hand.PokerHand = PokerHand.HighestCard;
                return _hand;
            }
        }

        private Boolean IsFlush()
        {
            Card previous = null;

            foreach (Card card in _hand.Cards)
            {
                if (previous != null)
                {
                    if (!previous.Suit.Equals(card.Suit))
                    {
                        return false;
                    }
                }

                previous = card;
            }

            return true;
        }

        //If there are more than one pair in the hand, we will count it as a pair.Two pairs isn’t valid, I don’t think we should ignore the pair.
        private bool IsPair()
        {
            return _duplicateCard?.Count == 2;
        }

        //Any more than two of the same card rank would count as three of a kind. Four of a kind is not valid, 
        //  I don’t think we should ignore the first three cards.
        private bool IsThreeOfAKind()
        {
            return _duplicateCard?.Count > 2;
        }

        //I'm assuming that we are looking for the highest count.
        private void GetMatchingCards()
        {
            _duplicateCard = _hand.Cards.GroupBy(x => x.Rank).Where(g => g.Count() > 1)
                                             .Select(x => new DuplicateCard { Rank = x.Key, Count = x.Count() })
                                             .OrderByDescending(x => x.Count)
                                             .ThenByDescending(x => x.Rank)
                                             .FirstOrDefault();
        }
    }

    public class DuplicateCard
    {
        public Ranks Rank { get; set; }
        public int Count { get; set; }
    }
}
