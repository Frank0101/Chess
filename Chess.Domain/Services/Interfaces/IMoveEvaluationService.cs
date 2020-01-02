using System.Collections.Generic;
using Chess.Domain.Enums;
using Chess.Domain.Models;
using Chess.Domain.Models.Moves;

namespace Chess.Domain.Services.Interfaces
{
    public interface IMoveEvaluationService
    {
        List<CpuMove> GetValidCpuMoves(Board board, PiecesColor turnColor);
    }
}
