using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerTest
{
    class Program
    {
        //I assume that in the event of a tie, we won't be comparing to find which hand has better cards  
        static void Main(string[] args)
        {
            PlayRound play = new PlayRound();

            string filePath = "c:\\temp\\round.txt";
            List<PlayersHand> playersHands = ReadPlayerInfo(filePath);
            string Winner = play.GetWinnerName(playersHands);

            Console.Write(Winner);
            //validate rank 16 or t
            //validate suit 20 or u
            //players with same name?
            //single player
        }

        private static string GetWinnerName(List<PlayersHand> playersHands)
        {
            throw new NotImplementedException();
        }

        private static List<PlayersHand> ReadPlayerInfo(string filePath)
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
            } catch(FileNotFoundException e)
            {
                Console.Write("No Player Info File Found.  Exiting program...");
                System.Environment.Exit(-1);
            }

            return playersHands;
        }
    }
}
