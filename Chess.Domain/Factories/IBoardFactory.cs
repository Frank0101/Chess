using Chess.Domain.Models;

namespace Chess.Domain.Factories
{
    public interface IBoardFactory
    {
        IBoard Create();
    }
}
