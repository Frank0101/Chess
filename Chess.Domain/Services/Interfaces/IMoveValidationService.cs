using Chess.Domain.Enums;
using Chess.Domain.Models;

namespace Chess.Domain.Services.Interfaces
{
    public interface IMoveValidationService
    {
        MoveValidationResult Validate(Move move);
    }
}
