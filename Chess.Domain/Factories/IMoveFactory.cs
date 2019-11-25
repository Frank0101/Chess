using Chess.Domain.Models;

namespace Chess.Domain.Factories
{
    public interface IMoveFactory
    {
        IMove Create(IBoard board, MoveDescriptor moveDescriptor);
    }
}
