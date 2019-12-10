using Chess.Domain.Models;

namespace Chess.Domain.Factories
{
    public class BoardFactory : IBoardFactory
    {
        private readonly IPieceFactory _pieceFactory;
        private readonly IMoveFactory _moveFactory;

        public BoardFactory(IPieceFactory pieceFactory, IMoveFactory moveFactory)
        {
            _pieceFactory = pieceFactory;
            _moveFactory = moveFactory;
        }

        public IBoard Create() =>
            new Board(_pieceFactory, _moveFactory);
    }
}
