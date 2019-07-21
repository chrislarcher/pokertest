using System;
using System.Collections.Generic;
using System.Linq;
using static PokerTest.PokerHands;

namespace PokerTest
{
    public class PlayRound
    {
        private List<PlayersHand> _playersHands = new List<PlayersHand>();
        private List<PlayersHand> _winner = new List<PlayersHand>();

        public string GetWinnerName(List<PlayersHand> playersHands)
        {
            _playersHands = playersHands;

           if (PlayersWith(PokerHand.Flush))
           {
                return GetWinners();
           }
           else if (PlayersWith(PokerHand.ThreeOfAKind))
           {
                return GetWinners();
           }
           else if (PlayersWith(PokerHand.Pair))
           {
                return GetWinners();
           }
           else
           {
                PlayerWithHighestCard(_playersHands);
                return GetWinners();
            }
        }

        private Boolean PlayersWith(PokerHand pokerHand)
        {
            Boolean playersFound = false;

            foreach (PlayersHand player in _playersHands)
            {
                if (player.GetHand() == pokerHand)
                {
                    _winner.Add(player);
                    playersFound = true;
                }
            }

            return playersFound;
        }

        private string GetWinners()
        {
            if (_winner.Count() == 1)
            {
                return _winner.FirstOrDefault().GetName();
            }

            PlayerWithHighestCard(_winner);
            string winners = "";

            foreach (PlayersHand player in _winner)
            {
                winners = winners == "" ? player.GetName() : winners + ", " + player.GetName();
            }

            return winners;
        }

        private void PlayerWithHighestCard(List<PlayersHand> playersHand)
        {
            List<PlayersHand> playerWithHighestCard = new List<PlayersHand>();

            foreach (PlayersHand player in playersHand)
            {
                if (playerWithHighestCard == null || playerWithHighestCard.Count == 0)
                {
                    playerWithHighestCard.Add(player);
                    continue;
                }
               
                if (player.GetHighestCard() > playerWithHighestCard.First().GetHighestCard())
                {
                    playerWithHighestCard = new List<PlayersHand>();
                    playerWithHighestCard.Add(player);
                }
                else if (player.GetHighestCard() == playerWithHighestCard.First().GetHighestCard())
                {
                    playerWithHighestCard.Add(player);
                }
            }

            _winner = playerWithHighestCard;
        }
    }
}
