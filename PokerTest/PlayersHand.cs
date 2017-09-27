using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PokerTest.PokerHands;

namespace PokerTest
{
    class PlayersHand
    {
        private string PlayersName;
        private List<Card> Cards = new List<Card>();
        private PokerHand Hand;

        public PlayersHand(string playerInfo)
        {
            string[] info = FormatPlayerInfo(playerInfo);

            if (info.Length != 6)
            {
                //making the assumption that if there is a bad record in the input file the game should terminate.
                Console.Write("Incorrect number of parameters.  Exiting Program...");
                System.Environment.Exit(-1);
            }

            SetPlayersHand(info);
        }

        public string GetName()
        {
            return PlayersName;
        }

        public PokerHand GetHand()
        {
            PokerHands pokerHands = new PokerHands();
            return pokerHands.GetPokerHand(Cards);
        }

        public int GetHighestCard()
        {
            int HighestCard = 0;

            foreach (Card card in Cards)
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
            PlayersName = info[0];

            for (int x = 1; x < info.Length; x++)
            {
                AddCard(info[x]);
            }
        }
        
        private void AddCard(string cardInfo)
        {
            Cards.Add(new Card(cardInfo));
        }
    }
}
