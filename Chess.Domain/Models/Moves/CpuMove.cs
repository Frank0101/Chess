namespace Chess.Domain.Models.Moves
{
    public class CpuMove : Move, ICpuMoveResponse
    {
        public int Value { get; set; }
        public ICpuMoveResponse? Response { get; set; }

        public CpuMove(int srcRow, int srcCol, int dstRow, int dstCol)
            : base(srcRow, srcCol, dstRow, dstCol)
        {
        }

        public override string ToString() =>
            $"{Src}{Dst}({Value}) -> {Response}";
    }
}
