using Chess.ConsoleApp.Services;
using Chess.Domain.Factories;
using Chess.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Chess.ConsoleApp
{
    public static class Program
    {
        public static void Main()
        {
            using var serviceProvider = RegisterDependencies();
            var application = serviceProvider.GetService<Application>();
            application.Start();
        }

        private static ServiceProvider RegisterDependencies()
        {
            return new ServiceCollection()
                .AddScoped<IGameFactory, GameFactory>()
                .AddScoped<IBoardFactory, BoardFactory>()
                .AddScoped<IConsoleWrapper, ConsoleWrapper>()
                .AddScoped<IConsoleService, ConsoleService>()
                .AddScoped<Application>()
                .BuildServiceProvider();
        }
    }
}
