using System;

namespace Chess.Domain.Models.Players
{
    public class CpuPlayer : IPlayer
    {
        public int RecursionLevel { get; }
        public Action OnBranchComputed { get; }

        public CpuPlayer(int recursionLevel, Action onBranchComputed)
        {
            RecursionLevel = recursionLevel;
            OnBranchComputed = onBranchComputed;
        }
    }
}
