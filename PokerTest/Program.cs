using System;
using System.Collections.Generic;
using System.IO;

namespace PokerTest
{
    class Program
    {
        //I assume that in the event of a tie, we won't be comparing to find which hand has better cards  
        static void Main(string[] args)
        {
            PlayRound play = new PlayRound();
            string filePath = Properties.Settings.Default.InputFile;

            try
            {
                PlayerHandsImport playersHandsImport = new PlayerHandsImport(filePath);
                List<PlayersHand> playersHands = playersHandsImport.GetPlayerHands();
                string Winner = play.GetWinnerName(playersHands);

                Console.Write(Winner);
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                System.Environment.Exit(-1);
            }
        }
    }
}
