namespace Chess.Domain.Models.Moves
{
    public class CpuMoveUnknown : ICpuMoveResponse
    {
        public decimal Value => 0;

        public override string ToString() => "...";
    }
}
