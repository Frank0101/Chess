using System;
using Chess.ConsoleApp.Enums;
using Chess.ConsoleApp.Services;
using Chess.Domain.Models.Games;

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
                {
                    var newGameConfig = _consoleService.RequestNewGameConfig();
                    var game = new UserVsCpuGame(newGameConfig.UserColor, newGameConfig.RecursionLevel);
                    _consoleService.DisplayBoard(game.Board, game.UserPlayer.Color);

                    break;
                }
                case MainMenuSelection.LoadGame:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
