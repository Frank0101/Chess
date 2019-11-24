using Chess.Domain.Enums;
using Chess.Domain.Models.Pieces;

namespace Chess.Domain.Factories
{
    public interface IPieceFactory
    {
        IPiece CreatePawn(PiecesColor color);
        IPiece CreateBishop(PiecesColor color);
        IPiece CreateKnight(PiecesColor color);
        IPiece CreateRook(PiecesColor color);
        IPiece CreateQueen(PiecesColor color);
        IPiece CreateKing(PiecesColor color);
    }
}
