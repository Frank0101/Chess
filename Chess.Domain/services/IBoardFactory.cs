using Chess.Domain.Models;

namespace Chess.Domain.services
{
    public interface IBoardFactory
    {
        Board Create();
    }
}
