namespace Chess.Domain.Models.Moves
{
    public class CpuMove : Move
    {
        public int Value { get; set; }
        public CpuMove? Response { get; set; }

        public CpuMove(int srcRow, int srcCol, int dstRow, int dstCol)
            : base(srcRow, srcCol, dstRow, dstCol)
        {
        }

        public override string ToString() =>
            $"{base.ToString()} -> {Response}";
    }
}
