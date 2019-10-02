namespace Chess.Domain.Models
{
    public class Move
    {
        public Tile SrcTile { get; }
        public Tile DstTile { get; }

        internal Move(Tile srcTile, Tile dstTile)
        {
            SrcTile = srcTile;
            DstTile = dstTile;
        }
    }
}
