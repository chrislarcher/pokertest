using PokerTest.Interfaces;
using PokerTest.Models;
using System;
using System.Linq;
using static PokerTest.Enums.CardTypes;
using static PokerTest.Enums.PokerHands;

namespace PokerTest.Services
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

        //guessing that we only care if its a flush, doesn't matter if its a straight flush or royal flush.
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

        //Making the assupmtion that if there are more than one pair in the hand, will still count as one pair.
        private bool IsPair()
        {
            return _duplicateCard?.Count == 2;
        }

        //Making the assumption that more than two of the same card counts as three of a kind.
        //  Four of a kind is not valid in this game. If it was added in the future, and the 
        //  the player had four of a kind, this code wouldn't trigger.
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
