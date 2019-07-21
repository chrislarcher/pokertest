using System;
using System.Collections.Generic;
using static PokerTest.PokerHands;

namespace PokerTest
{
    public class PlayersHand
    {
        private string _playersName;
        private List<Card> _cards = new List<Card>();

        public PlayersHand(string playerInfo)
        {
            string[] info = FormatPlayerInfo(playerInfo);

            if (info.Length != 6)
            {
                throw new FormatException("Incorrect number of parameters.");
            }

            SetPlayersHand(info);
        }

        public string GetName()
        {
            return _playersName;
        }

        public PokerHand GetHand()
        {
            PokerHands pokerHands = new PokerHands();
            return pokerHands.GetPokerHand(_cards);
        }

        public int GetHighestCard()
        {
            int HighestCard = 0;

            foreach (Card card in _cards)
            {
                if ((int)card.GetRank() > HighestCard)
                { 
                    HighestCard = (int)card.GetRank();
                }
            }

            return HighestCard;
        }

        private string[] FormatPlayerInfo(string playerInfo)
        {
            string info = playerInfo.Replace(" ", "");
            return info.Split(',');
        }

        private void SetPlayersHand(string[] info)
        {
            _playersName = info[0];

            for (int x = 1; x < info.Length; x++)
            {
                AddCard(info[x]);
            }
        }
        
        private void AddCard(string cardInfo)
        {
            _cards.Add(new Card(cardInfo));
        }
    }
}
