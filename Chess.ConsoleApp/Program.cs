using System.Threading.Tasks;
using Chess.ConsoleApp.Services;
using Chess.ConsoleApp.Services.Interfaces;
using Chess.Domain.Services;
using Chess.Domain.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Chess.ConsoleApp
{
    public static class Program
    {
        public static async Task Main()
        {
            await using var serviceProvider = RegisterDependencies();
            var applicationService = serviceProvider.GetService<IApplicationService>();
            await applicationService.Start();
        }

        private static ServiceProvider RegisterDependencies()
        {
            return new ServiceCollection()

                // console
                .AddScoped<IConsoleWrapper, ConsoleWrapper>()
                .AddScoped<IConsoleService, ConsoleService>()
                .AddScoped<IApplicationService, ApplicationService>()

                // domain
                .AddScoped<IMoveValidationService, MoveValidationService>()
                .AddScoped<IMoveEvaluationService, MoveEvaluationService>()
                .AddScoped<IUserPlayerService, UserPlayerService>()
                .AddScoped<ICpuPlayerService, CpuPlayerService>()
                .AddScoped<IGameService, GameService>()
                .BuildServiceProvider();
        }
    }
}
