using Chess.Domain.Models;

namespace Chess.Domain.Factories
{
    public class BoardFactory : IBoardFactory
    {
        private readonly IPieceFactory _pieceFactory;

        public BoardFactory(IPieceFactory pieceFactory)
        {
            _pieceFactory = pieceFactory;
        }

        public Board Create()
        {
            return new Board(_pieceFactory);
        }
    }
}
