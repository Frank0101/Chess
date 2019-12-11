namespace Chess.Domain.Models.Players
{
    public class CpuPlayer : IPlayer
    {
        public int RecursionLevel { get; }

        public CpuPlayer(int recursionLevel)
        {
            RecursionLevel = recursionLevel;
        }
    }
}
