using System;
using Chess.Domain.Enums;

namespace Chess.Domain.Models.Pieces
{
    public class Queen : Piece
    {
        public Queen(PiecesColor color)
            : base(color, color == PiecesColor.Black ? 'q' : 'Q', 10)
        {
        }

        public override bool IsMoveValid(MoveDescriptor moveDescriptor, bool eating)
        {
            var deltaRow = moveDescriptor.DstRow - moveDescriptor.SrcRow;
            var deltaCol = moveDescriptor.DstCol - moveDescriptor.SrcCol;

            return Math.Abs(deltaRow) == Math.Abs(deltaCol)
                   || deltaRow == 0 && deltaCol != 0
                   || deltaRow != 0 && deltaCol == 0;
        }
    }
}
