using Chess.Domain.Enums;

namespace Chess.Domain.Models.Pieces
{
    public class Queen : Piece
    {
        public Queen(PiecesColor color)
            : base(color, color == PiecesColor.White ? 'Q' : 'q', 10)
        {
        }
    }
}
