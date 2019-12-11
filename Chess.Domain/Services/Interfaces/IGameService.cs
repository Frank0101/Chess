using System.Threading.Tasks;
using Chess.Domain.Models;

namespace Chess.Domain.Services.Interfaces
{
    public interface IGameService
    {
        Task RunGame(Game game);
    }
}
