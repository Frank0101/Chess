using System;
using Chess.ConsoleApp.Enums;
using Chess.ConsoleApp.Services;
using Chess.Domain.Enums;
using Chess.Domain.Models;

namespace Chess.ConsoleApp
{
    public class Application
    {
        private readonly IConsoleService _consoleService;

        public Application(IConsoleService consoleService)
        {
            _consoleService = consoleService;
        }

        public void Start()
        {
            _consoleService.DisplayTitle();

            switch (_consoleService.RequestMainMenuSelection())
            {
                case MainMenuSelection.NewGame:
                    var newGameConfig = _consoleService.RequestNewGameConfig();
                    break;
                case MainMenuSelection.LoadGame:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            var board = new Board();
            Console.WriteLine(board);
            _consoleService.DisplayBoard(board, PiecesColor.Black);
            _consoleService.DisplayBoard(board, PiecesColor.White);
        }
    }
}
