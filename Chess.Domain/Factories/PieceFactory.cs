using Chess.Domain.Enums;
using Chess.Domain.Models.Pieces;

namespace Chess.Domain.Factories
{
    public class PieceFactory : IPieceFactory
    {
        public IPiece CreatePawn(PiecesColor color) =>
            new Pawn(color);

        public IPiece CreateBishop(PiecesColor color) =>
            new Bishop(color);

        public IPiece CreateKnight(PiecesColor color) =>
            new Knight(color);

        public IPiece CreateRook(PiecesColor color) =>
            new Rook(color);

        public IPiece CreateQueen(PiecesColor color) =>
            new Queen(color);

        public IPiece CreateKing(PiecesColor color) =>
            new King(color);
    }
}
