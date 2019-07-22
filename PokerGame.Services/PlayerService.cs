using System;
using System.Linq;
using PokerGame.Models;
using System.Collections.Generic;
using PokerGame.Services.Interfaces;


namespace PokerGame.Services
{
    public class PlayerService : IPlayerService
    {
        private ICardService _cardService;
        private IPokerHandService _pokerHandService;
        private Player _player;

        public PlayerService(ICardService cardService, IPokerHandService pokerHandService)
        {
            _cardService = cardService;
            _pokerHandService = pokerHandService;
        }

        public Player GetPlayer(string playerInfo)
        {
            _player = new Player();

            string[] info = FormatPlayerInfo(playerInfo);

            if (info.Length != 6)
            {
                throw new FormatException("Incorrect number of parameters.");
            }
            
            SetPlayersCards(info);
            SetPlayersHand();
            SortHand();

            return _player;
        }

        private string[] FormatPlayerInfo(string playerInfo)
        {
            string info = playerInfo.Replace(" ", "");
            return info.Split(',');
        }

        private void SetPlayersCards(string[] info)
        {
            _player.Name = info[0];
            _player.Hand = new Hand();
            _player.Hand.Cards = new List<Card>();

            for (int x = 1; x < info.Length; x++)
            {
                AddCard(info[x]);
            }
        }

        private void SetPlayersHand()
        {
            _player.Hand = _pokerHandService.GetPokerHand(_player.Hand);
        }

        private void AddCard(string cardInfo)
        {
            Card card = _cardService.GetCard(cardInfo);
            _player.Hand.Cards.Add(card);
        }

        private void SortHand()
        {
            _player.Hand.Cards = _player.Hand.Cards.OrderByDescending(x => x.Rank).ToList();
        }
    }
}
