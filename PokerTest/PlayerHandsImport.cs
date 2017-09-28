using System;
using System.Collections.Generic;
using System.IO;

namespace PokerTest
{
    static class PlayerHandsImport
    {
        public static List<PlayersHand> ReadPlayerInfo(string filePath)
        {
            List<PlayersHand> playersHands = new List<PlayersHand>();

            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        playersHands.Add(new PlayersHand(line));
                    }
                }
            }
            catch (Exception e)
            {
                Console.Write("No Player Info File Found.  Exiting program...");
                System.Environment.Exit(-1);
            }

            return playersHands;
        }
    }
}
