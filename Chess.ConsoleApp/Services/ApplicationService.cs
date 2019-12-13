using System;
using System.Threading.Tasks;
using Chess.ConsoleApp.Enums;
using Chess.ConsoleApp.Models.Commands;
using Chess.ConsoleApp.Services.Interfaces;
using Chess.Domain.Enums;
using Chess.Domain.Models;
using Chess.Domain.Models.Players;
using Chess.Domain.Services.Interfaces;

namespace Chess.ConsoleApp.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly IConsoleService _consoleService;
        private readonly IGameService _gameService;

        public ApplicationService(IConsoleService consoleService, IGameService gameService)
        {
            _consoleService = consoleService;
            _gameService = gameService;
        }

        public async Task Start()
        {
            _consoleService.DisplayTitle();

            await (_consoleService.GetMainMenuSelection() switch
            {
                MainMenuSelection.NewGame => HandleNewGame(),
                MainMenuSelection.LoadGame => throw new NotImplementedException(),
                _ => throw new NotImplementedException()
            });
        }

        private async Task HandleNewGame()
        {
            var (userColor, recursionLevel) = _consoleService.GetNewGameConfig();

            IPlayer userPlayer = new UserPlayer(OnMoveRequested);
            IPlayer cpuPlayer = new CpuPlayer(recursionLevel);

            var (whitePlayer, blackPlayer) = userColor == PiecesColor.White
                ? (userPlayer, cpuPlayer)
                : (cpuPlayer, userPlayer);

            var game = new Game(whitePlayer, blackPlayer, OnNewTurn);

            await _gameService.RunGame(game);
        }

        private Move? OnMoveRequested(Board board, PiecesColor turnColor)
        {
            _consoleService.DisplayCommandsMenu();

            return _consoleService.GetCommand() switch
            {
                MoveCommand moveCommand => ResolveMoveCommand(moveCommand),
                SaveCommand _ => null,
                QuitCommand _ => null,
                _ => throw new NotImplementedException()
            };
        }

        private static Move ResolveMoveCommand(MoveCommand moveCommand)
        {
            var (srcRow, srcCol, dstRow, dstCol)
                = (moveCommand.Value[1] - '1', moveCommand.Value[0] - 'a',
                    moveCommand.Value[3] - '1', moveCommand.Value[2] - 'a');

            return new Move(srcRow, srcCol, dstRow, dstCol);
        }

        private void OnNewTurn(Board board, PiecesColor turnColor) =>
            _consoleService.DisplayBoard(board, turnColor);
    }
}
