using Chess.Domain.Enums;
using Chess.Domain.Models;
using Chess.Domain.Services.Interfaces;

namespace Chess.Domain.Services
{
    public class MoveValidationService : IMoveValidationService
    {
        public MoveValidationResult Validate(Move move)
        {
            return MoveValidationResult.Valid;
        }
    }
}
