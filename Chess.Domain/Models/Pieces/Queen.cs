using Chess.Domain.Enums;

namespace Chess.Domain.Models.Pieces
{
    public class Queen : Piece
    {
        public Queen(PiecesColor color)
            : base(color, color == PiecesColor.Black ? 'q' : 'Q', 10)
        {
        }

        public override bool IsMoveValid(MoveDescriptor moveDescriptor)
        {
            return true;
        }
    }
}
