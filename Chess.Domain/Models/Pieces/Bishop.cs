using Chess.Domain.Enums;

namespace Chess.Domain.Models.Pieces
{
    public class Bishop : Piece
    {
        public Bishop(PiecesColor color)
            : base(color, color == PiecesColor.Black ? 'b' : 'B', 3)
        {
        }
    }
}
