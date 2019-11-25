using System;
using Chess.Domain.Enums;

namespace Chess.Domain.Models.Players
{
    public class UserPlayer : Player
    {
        public event Func<Board, IMove?>? MoveRequested;

        public UserPlayer(PiecesColor color) : base(color)
        {
        }

        public override bool TryMove(Board board)
        {
            var move = MoveRequested?.Invoke(board);
            return false;
        }
    }
}
