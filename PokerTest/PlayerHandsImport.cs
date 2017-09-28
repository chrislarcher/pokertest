using System;
using System.Collections.Generic;
using System.IO;

namespace PokerTest
{
    class PlayerHandsImport
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
                                        
                    while ((line = reader.ReadLine()) != null)
                    {

                        AddPlayersHand(line);
                    }
                }
            }
            catch (Exception)
            {
                Console.Write("No Player Info File Found.  Exiting program...");
                System.Environment.Exit(-1);
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
