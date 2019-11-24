using Chess.Domain.Enums;

namespace Chess.Domain.Models.Pieces
{
    public interface IPiece
    {
        PiecesColor Color { get; }
        char Symbol { get; }
        int Value { get; }

        bool IsMoveValid(MoveDescriptor moveDescriptor, bool eating);
        string ToString();
    }
}
