namespace Chess.Domain.Models
{
    public class MoveDescriptor
    {
        public int SrcRow { get; }
        public int SrcCol { get; }
        public int DstRow { get; }
        public int DstCol { get; }

        public MoveDescriptor(int srcRow, int srcCol, int dstRow, int dstCol)
        {
            SrcRow = srcRow;
            SrcCol = srcCol;
            DstRow = dstRow;
            DstCol = dstCol;
        }
    }
}
