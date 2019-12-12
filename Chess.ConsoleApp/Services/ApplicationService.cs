using System;
using System.Threading.Tasks;
using Chess.ConsoleApp.Enums;
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

            IPlayer userPlayer = new UserPlayer((board, turnColor) => null);
            IPlayer cpuPlayer = new CpuPlayer(recursionLevel);

            var (whitePlayer, blackPlayer) = userColor == PiecesColor.White
                ? (userPlayer, cpuPlayer)
                : (cpuPlayer, userPlayer);

            var game = new Game(whitePlayer, blackPlayer,
                (board, _) => { _consoleService.DisplayBoard(board, userColor); });

            await _gameService.RunGame(game);
        }
    }
}
