using System;
using System.Linq;
using System.Collections.Generic;
using PokerTest.Enums;
using PokerTest.Models;
using PokerTest.Interfaces;
using static PokerTest.Enums.PokerHands;

namespace PokerTest
{
    public class PokerService : IPokerService
    {
        private IPokerHandService _pokerHandService;
        private IPlayerService _playerService;
        private ICardService _cardService;

        private List<Player> _players = new List<Player>();
        private List<Player> _winners = new List<Player>();

        public PokerService(IPokerHandService pokerHandService, 
                            IPlayerService playerService, 
                            ICardService cardService)
        {
            _pokerHandService = pokerHandService;
            _playerService = playerService;
            _cardService = cardService;
        }

        public string GetWinnerName(List<Player> players)
        {
            _players = players;

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
                PlayerWithHighestCard(_players);
                return GetWinners();
            }
        }

        private Boolean PlayersWith(PokerHand pokerHand)
        {
            Boolean playersFound = false;

            foreach (Player player in _players)
            {
                PokerHand playersHand = _pokerHandService.GetPokerHand(player.Cards);

                if (playersHand == pokerHand)
                {
                    _winners.Add(player);
                    playersFound = true;
                }
            }

            return playersFound;
        }

        private string GetWinners()
        {
            if (_winners.Count() == 1)
            {
                return _winners.FirstOrDefault().Name;
            }

            PlayerWithHighestCard(_winners);
            string winners = "";

            foreach (Player player in _winners)
            {
                winners = winners == "" ? player.Name : winners + ", " + player.Name;
            }

            return winners;
        }

        private void PlayerWithHighestCard(List<Player> players)
        {
            List<Player> playerWithHighestCard = new List<Player>();

            foreach (Player player in players)
            {
                if (playerWithHighestCard == null || playerWithHighestCard.Count == 0)
                {
                    playerWithHighestCard.Add(player);
                    continue;
                }

                int playerHighCard = _cardService.GetHighestCard(player.Cards);
                int currentHighCard = _cardService.GetHighestCard(playerWithHighestCard.First().Cards);

                if (playerHighCard > currentHighCard)
                {
                    playerWithHighestCard = new List<Player>();
                    playerWithHighestCard.Add(player);
                }
                else if (playerHighCard == currentHighCard)
                {
                    playerWithHighestCard.Add(player);
                }
            }

            _winners = playerWithHighestCard;
        }
    }
}
