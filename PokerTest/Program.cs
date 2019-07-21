using PokerTest.Interfaces;
using PokerTest.Models;
using PokerTest.Services;
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
            ICardTypeService cardTypeService = new CardTypeService();
            ICardService cardService = new CardService(cardTypeService);
            IPlayerService playerService = new PlayerService(cardService);
            IPlayerImportService playerImportService = new PlayerImportService(playerService);
            IPokerHandService pokerHandService = new PokerHandService(cardService);
            IPokerService pokerService = new PokerService(pokerHandService, playerService, cardService);

            string filePath = Properties.Settings.Default.InputFile;

            try
            {
                List<Player> players = playerImportService.GetPlayers();
                string Winner = pokerService.GetWinnerName(players);

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
