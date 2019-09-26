using Chess.Domain.Enums;

namespace Chess.Domain.Models.Pieces
{
    public abstract class CastlingPiece : Piece
    {
        protected CastlingPiece(char symbol, int value, PiecesColor color)
            : base(symbol, value, color)
        {
        }
    }
}
