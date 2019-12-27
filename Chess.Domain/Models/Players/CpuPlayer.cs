using System;
using Chess.Domain.Models.Moves;

namespace Chess.Domain.Models.Players
{
    public class CpuPlayer : IPlayer
    {
        public int RecursionDepth { get; }
        public Action<int, Move, TimeSpan> OnMoveEvaluated { get; }

        public CpuPlayer(int recursionDepth, Action<int, Move, TimeSpan> onMoveEvaluated)
        {
            RecursionDepth = recursionDepth;
            OnMoveEvaluated = onMoveEvaluated;
        }
    }
}
