namespace Chess.Domain.Models
{
    public class Move
    {
        public int SrcRow { get; }
        public int SrcCol { get; }
        public int DstRow { get; }
        public int DstCol { get; }
        public int DeltaRow => DstRow - SrcRow;
        public int DeltaCol => DstCol - SrcCol;

        public Move(int srcRow, int srcCol, int dstRow, int dstCol)
        {
            SrcRow = srcRow;
            SrcCol = srcCol;
            DstRow = dstRow;
            DstCol = dstCol;
        }

        public override string ToString() =>
            $"{(char) ('a' + SrcCol)}{SrcRow + 1}"
            + $"{(char) ('a' + DstCol)}{DstRow + 1}";
    }
}
