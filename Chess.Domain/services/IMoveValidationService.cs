using Chess.Domain.Enums;
using Chess.Domain.Models;

namespace Chess.Domain.Services
{
    public interface IMoveValidationService
    {
        MoveValidationResult ValidateMove(Board board, MoveDescriptor moveDescriptor);
    }
}