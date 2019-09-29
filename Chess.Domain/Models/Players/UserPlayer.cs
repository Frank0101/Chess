using System;
using Chess.Domain.Enums;

namespace Chess.Domain.Models.Players
{
    public class UserPlayer : Player
    {
        public UserPlayer(PiecesColor color) : base(color)
        {
        }

        public override bool TryMove(Board board)
        {
            throw new NotImplementedException();
        }
    }
}
