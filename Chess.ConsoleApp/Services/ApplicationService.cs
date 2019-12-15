using System;
using System.Threading.Tasks;
using Chess.ConsoleApp.Enums;
using Chess.ConsoleApp.Models.Commands;
using Chess.ConsoleApp.Services.Interfaces;
using Chess.Domain.Enums;
using Chess.Domain.Models;
using Chess.Domain.Models.Moves;
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
            var (userColor, recursionDepth) = _consoleService.GetNewGameConfig();

            IPlayer userPlayer = new UserPlayer(
                (board, turnColor) =>
                    _consoleService.GetCommand() switch
                    {
                        MoveCommand moveCommand => ResolveMoveCommand(moveCommand),
                        SaveCommand _ => null,
                        QuitCommand _ => null,
                        _ => throw new NotImplementedException()
                    },
                (move, validationResult) =>
                    _consoleService.DisplayMoveValidationResult(validationResult));

            IPlayer cpuPlayer = new CpuPlayer(recursionDepth,
                recursionLevel =>
                    _consoleService.DisplayBranchComputed(recursionLevel));

            var (whitePlayer, blackPlayer) = userColor == PiecesColor.White
                ? (userPlayer, cpuPlayer)
                : (cpuPlayer, userPlayer);

            var game = new Game(whitePlayer, blackPlayer,
                (board, turnColor) =>
                {
                    _consoleService.DisplayBoard(board, userColor);
                    if (turnColor == userColor)
                    {
                        _consoleService.DisplayCommandsMenu();
                    }
                },
                (board, turnColor, move) =>
                {
                    _consoleService.DisplayBoard(board, userColor, move);
                    if (turnColor == userColor)
                    {
                        return _consoleService.GetMoveConfirmation();
                    }

                    _consoleService.WaitMoveAcknowledge();
                    return true;
                });

            await _gameService.RunGame(game);
        }

        private static Move ResolveMoveCommand(MoveCommand moveCommand)
        {
            var (srcRow, srcCol, dstRow, dstCol)
                = (moveCommand.Value[1] - '1', moveCommand.Value[0] - 'a',
                    moveCommand.Value[3] - '1', moveCommand.Value[2] - 'a');

            return new Move(srcRow, srcCol, dstRow, dstCol);
        }
    }
}
