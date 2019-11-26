using System;
using Chess.Domain.Enums;

namespace Chess.Domain.Models.Players
{
    public class UserPlayer : Player
    {
        public event Func<IBoard, IMove?>? MoveRequested;

        public UserPlayer(PiecesColor color) : base(color)
        {
        }

        public override bool TryGetMove(IBoard board, out IMove move)
        {
            move = MoveRequested?.Invoke(board);
            return move != null;
        }
    }
}
