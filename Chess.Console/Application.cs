using System;
using Chess.Console.Enums;
using Chess.Console.Services;
using Chess.Domain.Enums;
using Chess.Domain.Models;

namespace Chess.Console
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
            _consoleService.PrintTitle();

            switch (_consoleService.GetMainMenuSelection())
            {
                case MainMenuOption.NewGame:
                    var newGameConfig = _consoleService.GetNewGameMenuSelection();
                    break;
                case MainMenuOption.LoadGame:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            var board = new Board(PiecesColor.White);
            _consoleService.PrintBoard(board);
        }
    }
}
