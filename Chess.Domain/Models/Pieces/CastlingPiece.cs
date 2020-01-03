using Chess.Domain.Enums;

namespace Chess.Domain.Models.Pieces
{
    public abstract class CastlingPiece : Piece
    {
        protected CastlingPiece(PiecesColor color, char symbol, int value)
            : base(color, symbol, value)
        {
        }
    }
}
