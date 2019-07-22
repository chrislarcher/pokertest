using System;
using System.Linq;
using PokerGame.Models;
using System.Collections.Generic;
using PokerGame.Services.Interfaces;
using static PokerGame.Models.Enums.PokerHands;

namespace PokerGame.Services
{
    public class PokerService : IPokerService
    {
        private IHighestCardService _highestCardService;
        private IPlayerService _playerService;
        private ICardService _cardService;

        private List<Player> _players = new List<Player>();
        private List<Player> _winners = new List<Player>();

        public PokerService(IHighestCardService highestCardService,
                            IPlayerService playerService,
                            ICardService cardService)
        {
            _highestCardService = highestCardService;
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
                return GetWinners(true);
            }
            else if (PlayersWith(PokerHand.Pair))
            {
                return GetWinners(true);
            }
            else
            {
                _winners = _highestCardService.PlayerWithHighestCard(_players);
                return GetWinners();
            }
        }

        private Boolean PlayersWith(PokerHand pokerHand)
        {
            Boolean playersFound = false;

            foreach (Player player in _players)
            {
                if (player.Hand.PokerHand == pokerHand)
                {
                    _winners.Add(player);
                    playersFound = true;
                }
            }

            return playersFound;
        }

        private string GetWinners(bool ofAKind = false)
        {
            if (_winners.Count() == 1)
            {
                return _winners.FirstOrDefault().Name;
            }

            if (ofAKind)
            {
                _winners = _highestCardService.PlayerWithHighestPairs(_winners);

                if (_winners.Count() == 1 )
                {
                    return _winners.FirstOrDefault().Name;
                }
            }

            _winners = _highestCardService.PlayerWithHighestCard(_winners);
            string winners = "";

            foreach (Player player in _winners)
            {
                winners = winners == "" ? player.Name : winners + ", " + player.Name;
            }

            return winners;
        }
    }
}
