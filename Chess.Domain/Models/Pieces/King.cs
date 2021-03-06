using Chess.Domain.Enums;

namespace Chess.Domain.Models.Pieces
{
    public class King : CastlingPiece
    {
        public King(PiecesColor color)
            : base(color, color == PiecesColor.White ? 'K' : 'k', 0)
        {
        }
    }
}
