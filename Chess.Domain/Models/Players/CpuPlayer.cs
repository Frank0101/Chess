using Chess.Domain.Enums;

namespace Chess.Domain.Models.Players
{
    public class CpuPlayer : Player, ICpuPlayer
    {
        public int RecursionLevel { get; }

        public CpuPlayer(PiecesColor color, int recursionLevel) : base(color)
        {
            RecursionLevel = recursionLevel;
        }

        public override bool TryGetMove(IBoard board, out IMove move)
        {
            move = null;
            return false;
        }
    }
}
