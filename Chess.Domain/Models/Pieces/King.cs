namespace Chess.Domain.Models.Pieces
{
    public class King : CastlingPiece
    {
        public King(PiecesColor color) : base('♔', 99, color)
        {
        }
    }
}
