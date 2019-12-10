using Chess.Domain.Enums;

namespace Chess.Domain.Models.Pieces
{
    public class Rook : Piece
    {
        public Rook(PiecesColor color)
            : base(color, color == PiecesColor.Black ? 'r' : 'R', 5)
        {
        }
    }
}
