using System;
using Chess.ConsoleApp.Enums;
using Chess.ConsoleApp.Services;
using Chess.Domain.Services;

namespace Chess.ConsoleApp
{
    public class Application
    {
        private readonly IGameFactory _gameFactory;
        private readonly IConsoleService _consoleService;

        public Application(IGameFactory gameFactory, IConsoleService consoleService)
        {
            _gameFactory = gameFactory;
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
                    var game = _gameFactory.CreateUserVsCpuGame(userColor, recursionLevel);

                    game.TurnStarted += _ =>
                        _consoleService.DisplayBoard(game.Board, game.UserPlayer.Color);

                    game.UserPlayer.MoveRequested += board =>
                        _consoleService.TryRequestMoveSelection(game.Board, out var move) switch
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
