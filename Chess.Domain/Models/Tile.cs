using Chess.Domain.Models.Pieces;

namespace Chess.Domain.Models
{
    public class Tile
    {
        public IPiece? Piece { get; set; }

        public Tile(IPiece? piece)
        {
            Piece = piece;
        }

        public override string ToString()
        {
            return Piece?.ToString() ?? " ";
        }
    }
}
