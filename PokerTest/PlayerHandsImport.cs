using System;
using System.Collections.Generic;
using System.IO;

namespace PokerTest
{
    public class PlayerHandsImport
    {
        List<PlayersHand> _playersHands = new List<PlayersHand>();
        int _handCount = 0;

        public PlayerHandsImport(string filePath)
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

                        AddPlayersHand(playerHand);
                    }
                }
            }
            catch (FileNotFoundException)
            {
                throw new FileNotFoundException("No Player Info File Found.  Exiting program...");
            }
        }

        public List<PlayersHand> GetPlayerHands()
        {
            return _playersHands;
        }

        private void AddPlayersHand(string handInfo)
        {
            _handCount++;

            try
            {
                PlayersHand playersHand = new PlayersHand(handInfo);
                _playersHands.Add(playersHand);
            }
            catch (FormatException e)
            {
                Console.Write(e.Message + " Found at line " + _handCount);
            }
        }
    }
}
