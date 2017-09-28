using System;
using System.Collections.Generic;

namespace PokerTest
{
    class Program
    {
        //I assume that in the event of a tie, we won't be comparing to find which hand has better cards  
        static void Main(string[] args)
        {
            PlayRound play = new PlayRound();
            string filePath = Properties.Settings.Default.InputFile;
            PlayerHandsImport playersHandsImport = new PlayerHandsImport(filePath);

            List<PlayersHand> playersHands = playersHandsImport.GetPlayerHands();
            string Winner = play.GetWinnerName(playersHands);

            Console.Write(Winner);
        }
    }
}
