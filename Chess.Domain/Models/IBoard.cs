using Chess.Domain.Enums;

namespace Chess.Domain.Models
{
    public interface IBoard
    {
        PiecesColor TurnColor { get; }
        Tile this[int row, int col] { get; }

        MoveValidationResult TryCreateMove(MoveDescriptor moveDescriptor, out Move? move);
        void ApplyMove(Move move);
        string ToString();
    }
}
