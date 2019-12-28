using System;
using System.Collections.Generic;
using Chess.Domain.Models.Moves;

namespace Chess.Domain.Models.Players
{
    public class CpuPlayer : IPlayer
    {
        public int RecursionDepth { get; }
        public Action<int, List<CpuMove>, CpuMove, TimeSpan> OnMoveEvaluated { get; }

        public CpuPlayer(int recursionDepth,
            Action<int, List<CpuMove>, CpuMove, TimeSpan> onMoveEvaluated)
        {
            RecursionDepth = recursionDepth;
            OnMoveEvaluated = onMoveEvaluated;
        }
    }
}
