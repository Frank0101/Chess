using System;
using Chess.Domain.Enums;

namespace Chess.Domain.Models.Games
{
    public interface IGame
    {
        IBoard Board { get; }

        event Action<PiecesColor>? TurnStarted;

        void Start();
    }
}
