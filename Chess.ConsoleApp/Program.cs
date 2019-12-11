using System.Threading.Tasks;
using Chess.Domain.Models;
using Chess.Domain.Models.Players;
using Chess.Domain.Services;
using Chess.Domain.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Chess.ConsoleApp
{
    public static class Program
    {
        public static async Task Main()
        {
            var whitePlayer = new UserPlayer((board, turnColor) => null);
            var blackPlayer = new CpuPlayer(3);
            var game = new Game(whitePlayer, blackPlayer);

            await using var serviceProvider = RegisterDependencies();
            var gameService = serviceProvider.GetService<IGameService>();
            await gameService.RunGame(game);
        }

        private static ServiceProvider RegisterDependencies()
        {
            return new ServiceCollection()
                .AddScoped<IGameService, GameService>()
                .BuildServiceProvider();
        }
    }
}
