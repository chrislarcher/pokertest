using System;
using PokerGame.Models;
using PokerGame.Services;
using System.Collections.Generic;
using PokerGame.Services.Interfaces;


namespace PokerTest
{
    class Program
    {
        static void Main(string[] args)
        {
            ICardTypeService cardTypeService = new CardTypeService();
            ICardService cardService = new CardService(cardTypeService);
            IPokerHandService pokerHandService = new PokerHandService(cardService);
            IPlayerService playerService = new PlayerService(cardService, pokerHandService);
            IPlayerImportService playerImportService = new PlayerImportService(playerService);
            IHighestCardService highestCardService = new HighestCardService();
            IPokerService pokerService = new PokerService(highestCardService, playerService, cardService);

            string filePath = Properties.Settings.Default.InputFile;

            try
            {
                List<Player> players = playerImportService.GetPlayers(filePath);
                string Winner = pokerService.GetWinnerName(players);

                Console.Write(Winner);
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                Environment.Exit(-1);
            }
        }
    }
}
