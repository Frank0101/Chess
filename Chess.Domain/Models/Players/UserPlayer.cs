using System;
using Chess.Domain.Enums;

namespace Chess.Domain.Models.Players
{
    public class UserPlayer : IPlayer
    {
        public Func<Board, PiecesColor, Move?> OnMoveRequest { get; }
        public Action<Move?, MoveValidationResult> OnMoveValidated { get; }

        public UserPlayer(Func<Board, PiecesColor, Move?> onMoveRequest,
            Action<Move?, MoveValidationResult> onMoveValidated)
        {
            OnMoveRequest = onMoveRequest;
            OnMoveValidated = onMoveValidated;
        }
    }
}
