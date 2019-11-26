using Chess.Domain.Enums;

namespace Chess.Domain.Models.Players
{
    public interface IPlayer
    {
        PiecesColor Color { get; }

        bool TryGetMove(IBoard board, out IMove move);
    }
}
