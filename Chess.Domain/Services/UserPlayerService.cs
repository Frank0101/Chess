using Chess.Domain.Enums;
using Chess.Domain.Models;
using Chess.Domain.Models.Players;
using Chess.Domain.Services.Interfaces;

namespace Chess.Domain.Services
{
    public class UserPlayerService : IUserPlayerService
    {
        private readonly IMoveValidationService _moveValidationService;

        public UserPlayerService(IMoveValidationService moveValidationService)
        {
            _moveValidationService = moveValidationService;
        }

        public Move? GetMove(UserPlayer player, Board board, PiecesColor turnColor)
        {
            var move = player.OnMoveRequest(board, turnColor);
            if (move != null)
            {
                var validationResult = _moveValidationService.Validate(board, turnColor, move);
                player.OnMoveValidated(move, validationResult);

                return validationResult switch
                {
                    MoveValidationResult.Valid => move,
                    _ => GetMove(player, board, turnColor)
                };
            }

            return null;
        }
    }
}
