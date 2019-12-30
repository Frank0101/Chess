namespace Chess.Domain.Models.Moves
{
    public class CpuMoveCheckMate : ICpuMoveResponse
    {
        public int Value => -99;

        public override string ToString() => "MATE";
    }
}
