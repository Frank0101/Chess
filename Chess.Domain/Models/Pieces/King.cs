using Chess.Domain.Enums;

namespace Chess.Domain.Models.Pieces
{
    public class King : Piece
    {
        public King(PiecesColor color)
            : base(color, color == PiecesColor.Black ? 'k' : 'K', 99)
        {
        }
    }
}
