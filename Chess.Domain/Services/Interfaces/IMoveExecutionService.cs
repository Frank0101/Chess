using Chess.Domain.Models;
using Chess.Domain.Models.Moves;

namespace Chess.Domain.Services.Interfaces
{
    public interface IMoveExecutionService
    {
        void Execute(Board board, Move move);
    }
}
