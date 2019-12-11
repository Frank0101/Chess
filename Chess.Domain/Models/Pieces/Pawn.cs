using Chess.Domain.Enums;

namespace Chess.Domain.Models.Pieces
{
    public class Pawn : Piece
    {
        public Pawn(PiecesColor color)
            : base(color, color == PiecesColor.White ? 'P' : 'p', 1)
        {
        }
    }
}
