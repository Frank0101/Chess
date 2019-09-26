namespace Chess.Domain.Models.Pieces
{
    public class Pawn : Piece
    {
        public Pawn(PiecesColor color) : base('♙', 1, color)
        {
        }
    }
}
