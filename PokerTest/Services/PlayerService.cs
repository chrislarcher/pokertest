using System;
using PokerTest.Models;
using PokerTest.Interfaces;
using System.Collections.Generic;
using static PokerTest.Enums.PokerHands;

namespace PokerTest.Services
{
    public class PlayerService : IPlayerService
    {
        private ICardService _cardService;
        private Player _player;

        public PlayerService(ICardService cardService)
        {
            _cardService = cardService;
        }

        public Player GetPlayer(string playerInfo)
        {
            _player = new Player();

            string[] info = FormatPlayerInfo(playerInfo);

            if (info.Length != 6)
            {
                throw new FormatException("Incorrect number of parameters.");
            }

            SetPlayersHand(info);

            return _player;
        }

        private string[] FormatPlayerInfo(string playerInfo)
        {
            string info = playerInfo.Replace(" ", "");
            return info.Split(',');
        }

        private void SetPlayersHand(string[] info)
        {
            _player.Name = info[0];
            _player.Cards = new List<Card>();

            for (int x = 1; x < info.Length; x++)
            {
                AddCard(info[x]);
            }
        }
        
        private void AddCard(string cardInfo)
        {
            Card card = _cardService.GetCard(cardInfo);
            _player.Cards.Add(card);
        }
    }
}
