using Chess.Domain.Enums;
using Chess.Domain.Models;
using Chess.Domain.Models.Moves;

namespace Chess.Domain.Services.Interfaces
{
    public interface IMoveValidationService
    {
        MoveValidationResult Validate(Board board, PiecesColor turnColor, Move move);
        bool IsPositionUnderCheck(Board board, PiecesColor turnColor, Position position);
    }
}
