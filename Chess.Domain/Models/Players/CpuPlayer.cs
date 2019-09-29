using Chess.Domain.Enums;

namespace Chess.Domain.Models.Players
{
    public class CpuPlayer : Player
    {
        public int RecursionLevel { get; }

        public CpuPlayer(PiecesColor color, int recursionLevel) : base(color)
        {
            RecursionLevel = recursionLevel;
        }
    }
}
