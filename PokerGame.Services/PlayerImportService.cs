using System;
using System.IO;
using System.Collections.Generic;
using PokerGame.Services.Interfaces;
using PokerGame.Models;

namespace PokerGame.Services
{
    public class PlayerImportService : IPlayerImportService
    {
        private IPlayerService _playerService;
        List<Player> _players = new List<Player>();
        int _handCount = 0;

        public PlayerImportService(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        public List<Player> GetPlayers(string filePath)
        {
            
            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string playerHand;
                    if (reader.EndOfStream)
                    {
                        throw new FileLoadException("No Player Data Found.  Exiting program...");
                    }
                                        
                    while ((playerHand = reader.ReadLine()) != null)
                    {
                        playerHand = playerHand + ", " + reader.ReadLine();

                        AddPlayer(playerHand);
                    }
                }

                return _players;
            }
            catch (FileNotFoundException)
            {
                throw new FileNotFoundException("No Player Info File Found.  Exiting program...");
            }
        }

        private void AddPlayer(string playerInfo)
        {
            _handCount++;

            try
            {
                Player player = _playerService.GetPlayer(playerInfo);
                _players.Add(player);
            }
            catch (FormatException e)
            {
                Console.Write(e.Message + " Found at line " + _handCount);
            }
        }
    }
}
