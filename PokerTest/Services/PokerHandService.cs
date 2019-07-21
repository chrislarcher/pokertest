using PokerTest.Interfaces;
using PokerTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using static PokerTest.Enums.PokerHands;

namespace PokerTest.Services
{
    public class PokerHandService : IPokerHandService
    {
        private ICardService _cardService;
        private List<Card> _hand = new List<Card>();

        public PokerHandService(ICardService cardService)
        {
            _cardService = cardService;
        }

        public PokerHand GetPokerHand(List<Card> cards) 
        {
            _hand = cards;

            if (IsFlush())
            {
                return PokerHand.Flush;
            }
            else if(IsThreeOfAKind())
            {
                return PokerHand.ThreeOfAKind;
            }
            else if (IsPair())
            {
                return PokerHand.Pair;
            }
            else
            {
                return PokerHand.HighestCard;
            }
        }

        //guessing that we only care if its a flush, doesn't matter if its a straight flush or royal flush.
        private Boolean IsFlush()
        {
            Card previous = null;

            foreach (Card card in _hand)
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
        private Boolean IsPair()
        {
            return NumberOfMatchingCards() == 2;
        }

        //Making the assumption that I'm specifically checking for 3 matching cards.
        private Boolean IsThreeOfAKind()
        {
            return NumberOfMatchingCards() == 3;
        }

        //I'm assuming that we are looking for the highest count.
        private int NumberOfMatchingCards()
        {
            var cardCount = from card in _hand
                            group card by card.Rank into g
                    let count = g.Count()
                    orderby count descending
                    select new { Name = g.Key, Count = count, ID = g.First().Rank };

            return cardCount.First().Count;
        }
    }
}
