using System.Linq;
using PokerGame.Models;
using System.Collections.Generic;
using PokerGame.Services.Interfaces;
using static PokerGame.Models.Enums.CardTypes;

namespace PokerGame.Services
{
    public class HighestCardService : IHighestCardService
    {
        public List<Player> PlayerWithHighestPairs(List<Player> players)
        {
            List<Player> playerWithHighestCard = new List<Player>();

            Ranks highestRank = players.OrderByDescending(x => x.Hand.DuplicatesRank)
                                        .Select(d => d.Hand.DuplicatesRank)
                                        .FirstOrDefault();

            playerWithHighestCard = players.Where(x => x.Hand.DuplicatesRank == highestRank).ToList();

            foreach (Player player in playerWithHighestCard)
            {
                player.Hand.Cards = player.Hand.Cards.Where(x => x.Rank != highestRank).ToList();
            }

            return playerWithHighestCard;
        }

        public List<Player> PlayerWithHighestCard(List<Player> players)
        {
            int cardCount = players.FirstOrDefault().Hand.Cards.Count();
            List<Player> tiedPlayers = players;

            for (int count = 0; count < cardCount; count++)
            {
                tiedPlayers = players.Select(p => new Player
                {
                    Name = p.Name,
                    Hand = new Hand
                    {
                        Cards = p.Hand.Cards.Skip(count)
                                                    .Take(1)
                                                    .ToList()
                    }
                })
                .Where(p => tiedPlayers.Select(t => t.Name).ToList().Contains(p.Name))
                .ToList();

                tiedPlayers = HighestCard(tiedPlayers);

                if (tiedPlayers.Count() == 1)
                {
                    return tiedPlayers;
                }
            }

            return tiedPlayers;
        }

        private List<Player> HighestCard(List<Player> players)
        {
            List<Player> playerWithHighestCard = new List<Player>();

            Ranks highestRank = players.OrderByDescending(x => x.Hand.Cards.First().Rank)
                                       .Select(d => d.Hand.Cards.First().Rank)
                                       .FirstOrDefault();

            playerWithHighestCard = players.Where(x => x.Hand.Cards.First().Rank == highestRank).ToList();

            return playerWithHighestCard;
        }
    }
}
