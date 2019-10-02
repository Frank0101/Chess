using Chess.Domain.Enums;

namespace Chess.Domain.Models.Pieces
{
    public class Rook : CastlingPiece
    {
        public Rook(PiecesColor color)
            : base(color, color == PiecesColor.Black ? 'r' : 'R', 5)
        {
        }

        public override bool IsMoveValid(int srcRow, int srcCol, int dstRow, int dstCol)
        {
            return true;
        }
    }
}
