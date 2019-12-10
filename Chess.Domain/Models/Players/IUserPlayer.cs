using System;

namespace Chess.Domain.Models.Players
{
    public interface IUserPlayer : IPlayer
    {
        event Func<IBoard, IMove?>? MoveRequested;
    }
}
