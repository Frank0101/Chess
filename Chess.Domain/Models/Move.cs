namespace Chess.Domain.Models
{
    public class Move
    {
        public int SrcRow { get; }
        public int SrcCol { get; }
        public int DstRow { get; }
        public int DstCol { get; }
        public int Value { get; set; }
        public Move? Next { get; set; }

        public Move(int srcRow, int srcCol, int dstRow, int dstCol)
        {
            SrcRow = srcRow;
            SrcCol = srcCol;
            DstRow = dstRow;
            DstCol = dstCol;
        }

        public override string ToString() =>
            $"${'a' + SrcCol}${SrcRow + 1}${'a' + DstCol}${DstRow + 1}";
    }
}
