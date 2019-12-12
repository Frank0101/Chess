using System;
using System.Threading.Tasks;
using Chess.ConsoleApp.Services.Interfaces;
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
            var whitePlayer = new UserPlayer((board, turnColor) => null);
            var blackPlayer = new CpuPlayer(3);
            var game = new Game(whitePlayer, blackPlayer, (board, turnColor) =>
            {
                Console.WriteLine(board);
                Console.WriteLine(turnColor);
            });

            await _gameService.RunGame(game);
        }
    }
}
