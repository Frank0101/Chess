using Chess.Domain.Enums;
using Chess.Domain.Models.Games;

namespace Chess.Domain.Factories
{
    public interface IGameFactory
    {
        IUserVsCpuGame CreateUserVsCpuGame(PiecesColor userColor, int recursionLevel);
    }
}
