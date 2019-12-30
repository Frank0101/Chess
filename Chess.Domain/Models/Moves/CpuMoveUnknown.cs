namespace Chess.Domain.Models.Moves
{
    public class CpuMoveUnknown : ICpuMoveResponse
    {
        public int Value => 0;

        public override string ToString() => "...";
    }
}
