using System;

namespace Chess.Domain.Models.Players
{
    public class CpuPlayer : IPlayer
    {
        public int RecursionDepth { get; }
        public Action<int, TimeSpan> OnBranchComputed { get; }

        public CpuPlayer(int recursionDepth, Action<int, TimeSpan> onBranchComputed)
        {
            RecursionDepth = recursionDepth;
            OnBranchComputed = onBranchComputed;
        }
    }
}
