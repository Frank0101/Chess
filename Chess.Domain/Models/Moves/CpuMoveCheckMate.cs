namespace Chess.Domain.Models.Moves
{
    public class CpuMoveCheckMate : ICpuMove
    {
        public int Value => -99;

        public override string ToString() => "MATE";
    }
}
