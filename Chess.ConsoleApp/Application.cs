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
                    var (userColor, recursionLevel) = _consoleService.RequestNewGameConfig();
                    var game = new UserVsCpuGame(userColor, recursionLevel);

                    game.TurnStarted += _ =>
                        _consoleService.DisplayBoard(game.Board, game.UserPlayer.Color);

                    game.UserPlayer.MoveRequested += board =>
                        _consoleService.RequestMoveSelection(out var move) switch
                        {
                            MoveSelection.Move => move,
                            MoveSelection.SaveGame => null,
                            MoveSelection.ExitGame => null,
                            _ => null
                        };

                    game.Start();

                    break;
                }
                case MainMenuSelection.LoadGame:
                    break;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
