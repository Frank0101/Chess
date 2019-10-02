using Chess.Domain.Enums;

namespace Chess.Domain.Models.Pieces
{
    public class Pawn : Piece
    {
        public Pawn(PiecesColor color)
            : base(color, color == PiecesColor.Black ? 'p' : 'P', 1)
        {
        }

        public override bool IsMoveValid(int srcRow, int srcCol, int dstRow, int dstCol)
        {
            return true;
        }
    }
}
