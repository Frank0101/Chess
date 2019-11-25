using Chess.Domain.Models;

namespace Chess.Domain.Factories
{
    public class MoveFactory : IMoveFactory
    {
        public IMove Create(IBoard board, MoveDescriptor moveDescriptor) =>
            new Move(board, moveDescriptor);
    }
}
