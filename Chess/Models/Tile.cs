using Chess.Models.Pieces;

namespace Chess.Models
{
    public class Tile
    {
        public Piece? Piece { get; set; }

        public Tile(Piece? piece)
        {
            Piece = piece;
        }

        public override string ToString()
        {
            return Piece?.ToString() ?? " ";
        }
    }
}
