using Chess.Domain.Enums;
using Chess.Domain.Models.Players;

namespace Chess.Domain.Factories
{
    public interface IPlayerFactory
    {
        IPlayer CreateUserPlayer(PiecesColor color);
        IPlayer CreateCpuPlayer(PiecesColor color, int recursionLevel);
    }
}
