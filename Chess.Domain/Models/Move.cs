namespace Chess.Domain.Models
{
    public class Move
    {
        public Tile SrcTile { get; }
        public Tile DstTile { get; }

        internal Move(Board board, MoveDescriptor moveDescriptor)
        {
            SrcTile = board[moveDescriptor.SrcRow, moveDescriptor.SrcCol];
            DstTile = board[moveDescriptor.DstRow, moveDescriptor.DstCol];
        }
    }
}
