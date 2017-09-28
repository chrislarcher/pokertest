using System;
using System.Collections.Generic;

namespace PokerTest
{
    class Program
    {
        //I assume that in the event of a tie, we won't be comparing to find which hand has better cards  
        static void Main(string[] args)
        {
            string filePath = Properties.Settings.Default.InputFile;
            
            PlayRound play = new PlayRound();
                    
            List<PlayersHand> playersHands = PlayerHandsImport.ReadPlayerInfo(filePath);
            string Winner = play.GetWinnerName(playersHands);

            Console.Write(Winner);
        }
    }
}
