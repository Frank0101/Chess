using Chess.Domain.Models.Players;

namespace Chess.Domain.Models.Games
{
    public interface IUserVsCpuGame : IGame
    {
        IUserPlayer UserPlayer { get; }
        ICpuPlayer CpuPlayer { get; }
    }
}
