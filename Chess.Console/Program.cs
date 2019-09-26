using Chess.Domain.Models;
using Chess.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Chess
{
    public class Program
    {
        public static void Main(string[] args)
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
