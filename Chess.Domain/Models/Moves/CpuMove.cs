namespace Chess.Domain.Models.Moves
{
    public class CpuMove : Move
    {
        public int Value { get; }

        public CpuMove(int srcRow, int srcCol, int dstRow, int dstCol, int value)
            : base(srcRow, srcCol, dstRow, dstCol)
        {
            Value = value;
        }
    }
}
