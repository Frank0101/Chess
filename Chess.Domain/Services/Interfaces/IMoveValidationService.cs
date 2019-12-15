using Chess.Domain.Enums;
using Chess.Domain.Models;

namespace Chess.Domain.Services.Interfaces
{
    public interface IMoveValidationService
    {
        MoveValidationResult Validate(Board board, PiecesColor turnColor, Move move);
    }
}
