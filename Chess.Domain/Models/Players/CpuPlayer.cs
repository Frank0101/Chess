using System;

namespace Chess.Domain.Models.Players
{
    public class CpuPlayer : IPlayer
    {
        public int RecursionDepth { get; }
        public Action<int> OnBranchComputed { get; }

        public CpuPlayer(int recursionDepth, Action<int> onBranchComputed)
        {
            RecursionDepth = recursionDepth;
            OnBranchComputed = onBranchComputed;
        }
    }
}
