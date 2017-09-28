using System;
using System.Collections.Generic;
using System.IO;

namespace PokerTest
{
    public class PlayerHandsImport
    {
        List<PlayersHand> playersHands = new List<PlayersHand>();
        int handCount = 0;

        public PlayerHandsImport(string filePath)
        {
            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    if (reader.EndOfStream)
                    {
                        throw new FileLoadException("No Player Data Found.  Exiting program...");
                    }
                                        
                    while ((line = reader.ReadLine()) != null)
                    {
                        AddPlayersHand(line);
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
            return playersHands;
        }

        private void AddPlayersHand(string handInfo)
        {
            handCount++;

            try
            {
                PlayersHand playersHand = new PlayersHand(handInfo);
                playersHands.Add(playersHand);
            }
            catch (FormatException e)
            {
                Console.Write(e.Message + " Found at line " + handCount);
            }
        }
    }
}
