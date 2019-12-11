using Chess.Domain.Enums;

namespace Chess.Domain.Models.Pieces
{
    public class Knight : Piece
    {
        public Knight(PiecesColor color)
            : base(color, color == PiecesColor.White ? 'N' : 'n', 4)
        {
        }
    }
}
