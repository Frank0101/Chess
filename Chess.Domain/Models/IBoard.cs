using Chess.Domain.Enums;

namespace Chess.Domain.Models
{
    public interface IBoard
    {
        PiecesColor TurnColor { get; }
        int TurnIndex { get; }

        Tile this[int row, int col] { get; }

        MoveValidationResult TryCreateMove(MoveDescriptor moveDescriptor, out IMove? move);
        void ApplyMove(IMove move);
        IMove UndoLastMove();
        string ToString();
    }
}
