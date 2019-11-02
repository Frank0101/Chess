using Chess.Domain.Enums;

namespace Chess.Domain.Models.Pieces
{
    public class Rook : CastlingPiece
    {
        public Rook(PiecesColor color)
            : base(color, color == PiecesColor.Black ? 'r' : 'R', 5)
        {
        }

        public override bool IsMoveValid(MoveDescriptor moveDescriptor)
        {
            return true;
        }
    }
}
