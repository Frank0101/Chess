namespace Chess.Domain.Models.Moves
{
    public class CpuMoveCheckMate : ICpuMoveResponse
    {
        public decimal Value => -99;

        public override string ToString() => "MATE";
    }
}
