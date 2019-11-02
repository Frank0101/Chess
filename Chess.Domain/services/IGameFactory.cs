using Chess.Domain.Enums;
using Chess.Domain.Models.Games;

namespace Chess.Domain.Services
{
    public interface IGameFactory
    {
        UserVsCpuGame CreateUserVsCpuGame(PiecesColor userColor, int recursionLevel);
    }
}
