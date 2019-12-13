using Chess.Domain.Enums;
using Chess.Domain.Models;
using Chess.Domain.Models.Players;
using Chess.Domain.Services.Interfaces;

namespace Chess.Domain.Services
{
    public class UserPlayerService : IUserPlayerService
    {
        public Move? GetMove(UserPlayer player, Board board, PiecesColor turnColor)
        {
            var move = player.OnMoveRequested(board, turnColor);
            player.OnMoveValidated(move, MoveValidationResult.Valid);
            return move;
        }
    }
}
