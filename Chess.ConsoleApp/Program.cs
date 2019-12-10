using Microsoft.Extensions.DependencyInjection;

namespace Chess.ConsoleApp
{
    public static class Program
    {
        public static void Main()
        {
            using var serviceProvider = RegisterDependencies();
        }

        private static ServiceProvider RegisterDependencies()
        {
            return new ServiceCollection()
                .BuildServiceProvider();
        }
    }
}
