using Chess.Domain.Models;

namespace Chess.Domain.Services
{
    public interface IBoardFactory
    {
        Board Create();
    }
}
