using System;
using System.Collections.Generic;
using System.Linq;

namespace PokerTest
{
    class PokerHands
    {
        public enum PokerHand { Flush, ThreeOfAKind, Pair, HighestCard }

        private List<Card> Hand = new List<Card>();

        public PokerHand GetPokerHand(List<Card> cards) 
        {
            Hand = cards;

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

            foreach (Card card in Hand)
            {
                if (previous != null)
                {
                    if (!previous.GetSuit().Equals(card.GetSuit()))
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
            return 2 == NumberOfMatchingCards();
        }

        //I'm making an assumption that anything over 3 of the same kind is three of a kind.
            //There is no four of a kind defined.
        private Boolean IsThreeOfAKind()
        {
            return NumberOfMatchingCards() > 2;
        }

        //I'm assuming that we are looking for the highest count.
        private int NumberOfMatchingCards()
        {
            var cardCount = from card in Hand
                    group card by card.GetRank() into g
                    let count = g.Count()
                    orderby count descending
                    select new { Name = g.Key, Count = count, ID = g.First().GetRank() };

            return cardCount.First().Count;
        }
    }
}
