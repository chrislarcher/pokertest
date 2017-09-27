using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PokerTest.PokerHands;

namespace PokerTest
{
    class PlayRound
    {
        private List<PlayersHand> PlayersHands = new List<PlayersHand>();
        private string Winner;

        public string GetWinnerName(List<PlayersHand> playersHands)
        {
            PlayersHands = playersHands;

           if (PlayersWith(PokerHand.Flush))
           {
                return Winner;
           }
           else if (PlayersWith(PokerHand.ThreeOfAKind))
           {
                return Winner;
           }
           else if (PlayersWith(PokerHand.Pair))
           {
                return Winner;
           }
           else
            {
                return PlayerWithHighestCard();
            }
        }

        private Boolean PlayersWith(PokerHand pokerHand)
        {
            Boolean playersFound = false;

            foreach (PlayersHand player in PlayersHands)
            {
                if (player.GetHand() == pokerHand)
                {
                    Winner = Winner + " " + player.GetName();
                    playersFound = true;
                }
            }

            return playersFound;
        }

        private string PlayerWithHighestCard()
        {
            List<PlayersHand> playerWithHighestCard = new List<PlayersHand>();

            foreach (PlayersHand player in PlayersHands)
            {
                if (playerWithHighestCard != null)
                {
                    if (player.GetHighestCard() > playerWithHighestCard[0].GetHighestCard())
                    {
                        playerWithHighestCard = new List<PlayersHand>();
                        playerWithHighestCard.Add(player);
                    }
                    else if (player.GetHighestCard() == playerWithHighestCard[0].GetHighestCard())
                    {
                        playerWithHighestCard.Add(player);
                    }
                }
            }

            foreach (PlayersHand player in playerWithHighestCard)
            {
                Winner = Winner + " " + player.GetName();
            }

            return Winner;
        }
    }
}
