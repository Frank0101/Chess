using Chess.Domain.Enums;

namespace Chess.Domain.Models.Pieces
{
    public class Bishop : Piece
    {
        public Bishop(PiecesColor color)
            : base(color, color == PiecesColor.Black ? 'b' : 'B', 3)
        {
        }

        public override bool IsMoveValid(int srcRow, int srcCol, int dstRow, int dstCol)
        {
            return true;
        }
    }
}
