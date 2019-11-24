using System;
using Chess.Domain.Enums;

namespace Chess.Domain.Models.Pieces
{
    public class Knight : Piece
    {
        public Knight(PiecesColor color)
            : base(color, color == PiecesColor.Black ? 'n' : 'N', 4)
        {
        }

        public override bool IsMoveValid(MoveDescriptor moveDescriptor, bool eating)
        {
            var deltaRow = moveDescriptor.DstRow - moveDescriptor.SrcRow;
            var deltaCol = moveDescriptor.DstCol - moveDescriptor.SrcCol;

            return Math.Abs(deltaRow) == 1 && Math.Abs(deltaCol) == 2
                   || Math.Abs(deltaRow) == 2 && Math.Abs(deltaCol) == 1;
        }
    }
}
