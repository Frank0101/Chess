using Chess.Domain.Enums;
using Chess.Domain.Models.Players;

namespace Chess.Domain.Factories
{
    public class PlayerFactory : IPlayerFactory
    {
        public IPlayer CreateUserPlayer(PiecesColor color) =>
            new UserPlayer(color);

        public IPlayer CreateCpuPlayer(PiecesColor color, int recursionLevel) =>
            new CpuPlayer(color, recursionLevel);
    }
}
