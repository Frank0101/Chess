using System.Threading.Tasks;
using Chess.Domain.Models;

namespace Chess.Domain.Services.Interfaces
{
    public interface IGameService
    {
        ValueTask RunGame(Game game);
    }
}
