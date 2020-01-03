using Chess.Domain.Enums;

namespace Chess.Domain.Models.Pieces
{
    public class Rook : CastlingPiece
    {
        public Rook(PiecesColor color)
            : base(color, color == PiecesColor.White ? 'R' : 'r', 5)
        {
        }
    }
}
