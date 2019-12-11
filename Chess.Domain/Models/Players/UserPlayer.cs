using System;
using Chess.Domain.Enums;

namespace Chess.Domain.Models.Players
{
    public class UserPlayer : IPlayer
    {
        public Func<Board, PiecesColor, Move?>? OnMoveRequested { get; }

        public UserPlayer(Func<Board, PiecesColor, Move?>? OnMoveRequested)
        {
            this.OnMoveRequested = OnMoveRequested;
        }
    }
}
