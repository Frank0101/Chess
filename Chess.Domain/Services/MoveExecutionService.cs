using Chess.Domain.Models;
using Chess.Domain.Models.Moves;
using Chess.Domain.Services.Interfaces;

namespace Chess.Domain.Services
{
    public class MoveExecutionService : IMoveExecutionService
    {
        public void Execute(Board board, Move move)
        {
            board[move.Dst] = board[move.Src];
            board[move.Src] = null;
        }
    }
}
