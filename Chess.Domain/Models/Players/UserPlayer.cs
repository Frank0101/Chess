using System;
using Chess.Domain.Enums;

namespace Chess.Domain.Models.Players
{
    public class UserPlayer : IPlayer
    {
        public Func<Board, PiecesColor, Move?> OnMoveRequested { get; }
        public Action<Move?, MoveValidationResult> OnMoveValidated { get; }

        public UserPlayer(Func<Board, PiecesColor, Move?> onMoveRequested,
            Action<Move?, MoveValidationResult> onMoveValidated)
        {
            OnMoveRequested = onMoveRequested;
            OnMoveValidated = onMoveValidated;
        }
    }
}
