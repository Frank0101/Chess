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

        public ApplicationService(
            IConsoleService consoleService,
            IGameService gameService)
        {
            _consoleService = consoleService;
            _gameService = gameService;
        }

        public async Task Start()
        {
            _consoleService.DisplayTitle();

            await (_consoleService.GetMainMenuSelection() switch
            {
                //MainMenuSelection.NewGame => HandleNewUserVsCpuGame(),
                MainMenuSelection.NewGame => HandleNewUserVsUserGame(),
                //MainMenuSelection.NewGame => HandleNewCpuVsCpuGame(),
                MainMenuSelection.LoadGame => throw new NotImplementedException(),
                _ => throw new NotImplementedException()
            });
        }

        private async Task HandleNewUserVsCpuGame()
        {
            var (userColor, recursionDepth) = _consoleService.GetNewGameConfig();

            IPlayer userPlayer = new UserPlayer(
                (board, turnColor) =>
                    _consoleService.GetCommand() switch
                    {
                        MoveCommand moveCommand => moveCommand.Move,
                        SaveCommand _ => null,
                        QuitCommand _ => null,
                        _ => throw new NotImplementedException()
                    },
                (move, validationResult) =>
                    _consoleService.DisplayMoveValidationResult(validationResult));

            IPlayer cpuPlayer = new CpuPlayer(recursionDepth,
                (recursionLevel, moves, bestMove, time) =>
                    _consoleService.DisplayBranchComputed(recursionLevel,
                        moves, bestMove, time));

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

        private async Task HandleNewUserVsUserGame()
        {
            IPlayer whitePlayer = new UserPlayer(
                (board, turnColor) =>
                    _consoleService.GetCommand() switch
                    {
                        MoveCommand moveCommand => moveCommand.Move,
                        SaveCommand _ => null,
                        QuitCommand _ => null,
                        _ => throw new NotImplementedException()
                    },
                (move, validationResult) =>
                    _consoleService.DisplayMoveValidationResult(validationResult));

            IPlayer blackPlayer = new UserPlayer(
                (board, turnColor) =>
                    _consoleService.GetCommand() switch
                    {
                        MoveCommand moveCommand => moveCommand.Move,
                        SaveCommand _ => null,
                        QuitCommand _ => null,
                        _ => throw new NotImplementedException()
                    },
                (move, validationResult) =>
                    _consoleService.DisplayMoveValidationResult(validationResult));

            var game = new Game(whitePlayer, blackPlayer,
                (board, turnColor) =>
                {
                    _consoleService.DisplayBoard(board, turnColor);
                    _consoleService.DisplayCommandsMenu();
                },
                (board, turnColor, move) =>
                {
                    _consoleService.DisplayBoard(board, turnColor, move);
                    return _consoleService.GetMoveConfirmation();
                });

            await _gameService.RunGame(game);
        }

        private async Task HandleNewCpuVsCpuGame()
        {
            IPlayer whitePlayer = new CpuPlayer(2,
                (recursionLevel, moves, bestMove, time) =>
                    _consoleService.DisplayBranchComputed(recursionLevel,
                        moves, bestMove, time));

            IPlayer blackPlayer = new CpuPlayer(2,
                (recursionLevel, moves, bestMove, time) =>
                    _consoleService.DisplayBranchComputed(recursionLevel,
                        moves, bestMove, time));

            var game = new Game(whitePlayer, blackPlayer,
                (board, turnColor) =>
                {
                    _consoleService.DisplayBoard(board, PiecesColor.White);
                },
                (board, turnColor, move) => true);

            await _gameService.RunGame(game);
        }
    }
}
