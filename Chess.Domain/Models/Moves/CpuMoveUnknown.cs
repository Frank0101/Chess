namespace Chess.Domain.Models.Moves
{
    public class CpuMoveUnknown : ICpuMove
    {
        public int Value => 0;

        public override string ToString() => "...";
    }
}
