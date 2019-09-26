using Chess.Console.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Chess.Console
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
                .AddScoped<IConsoleService, ConsoleService>()
                .AddScoped<Application>()
                .BuildServiceProvider();
        }
    }
}
