using Chess.Domain.Enums;
using Chess.Domain.Models;
using Chess.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Chess
{
    public static class Program
    {
        public static void Main()
        {
            var serviceCollection = new ServiceCollection()
                .AddScoped<IConsoleService, ConsoleService>()
                .BuildServiceProvider();

            using (serviceCollection)
            {
                var consoleService = serviceCollection.GetService<IConsoleService>();

                var board = new Board(PiecesColor.White);
                consoleService.PrintBoard(board);
            }
        }
    }
}
