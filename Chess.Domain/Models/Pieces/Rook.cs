using Chess.Domain.Enums;

namespace Chess.Domain.Models.Pieces
{
    public class Rook : CastlingPiece
    {
        public Rook(PiecesColor color)
            : base(color, color == PiecesColor.Black ? 'r' : 'R', 5)
        {
        }

        public override bool IsMoveValid(MoveDescriptor moveDescriptor, bool eating)
        {
            var deltaRow = moveDescriptor.DstRow - moveDescriptor.SrcRow;
            var deltaCol = moveDescriptor.DstCol - moveDescriptor.SrcCol;

            return deltaRow == 0 && deltaCol != 0
                   || deltaRow != 0 && deltaCol == 0;
        }
    }
}
