using System;
using System.Linq;
using System.Collections.Generic;
using PokerTest.Models;
using PokerTest.Interfaces;
using static PokerTest.Enums.PokerHands;
using static PokerTest.Enums.CardTypes;

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
                return GetWinners(true);
            }
            else if (PlayersWith(PokerHand.Pair))
            {
                return GetWinners(true);
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
                _winners.
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
            int cardCount = players.FirstOrDefault().Hand.Cards.Count();
            List<Player> tiedPlayers = players;

            for (int count = 0; count < cardCount; count++)
            {
                tiedPlayers = players.Select(p => new Player
                {
                    Name = p.Name,
                    Hand = p.Hand.Cards.Skip(count)
                                   .Take(1)
                                   .ToList()
                })
                .Where(p => tiedPlayers.Select(t=> t.Name).ToList().Contains(p.Name))
                .ToList();

                tiedPlayers = HighestCard(tiedPlayers);

                if (tiedPlayers.Count()==1)
                {
                    _winners = tiedPlayers;
                    return;
                }
            }

            _winners = tiedPlayers;
        }

        private List<Player> HighestCard(List<Player> players)
        {
            List<Player> playerWithHighestCard = new List<Player>();

            foreach (Player player in players)
            {
                if (playerWithHighestCard == null || playerWithHighestCard.Count == 0)
                {
                    playerWithHighestCard.Add(player);
                    continue;
                }
                /*TODO: DOn't need to use the get highest card*/
                int playerHighCard = _cardService.GetHighestCard(player.Hand.Cards);
                int currentHighCard = _cardService.GetHighestCard(playerWithHighestCard.First().Hand.Cards);

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

            return playerWithHighestCard;
        }
    }
}
