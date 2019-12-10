using Chess.Domain.Enums;
using Chess.Domain.Models.Players;

namespace Chess.Domain.Factories
{
    public interface IPlayerFactory
    {
        IUserPlayer CreateUserPlayer(PiecesColor color);
        ICpuPlayer CreateCpuPlayer(PiecesColor color, int recursionLevel);
    }
}
