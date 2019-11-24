using System;
using Chess.Domain.Enums;

namespace Chess.Domain.Models.Pieces
{
    public class Bishop : Piece
    {
        public Bishop(PiecesColor color)
            : base(color, color == PiecesColor.Black ? 'b' : 'B', 3)
        {
        }

        public override bool IsMoveValid(MoveDescriptor moveDescriptor, bool eating) =>
            Math.Abs(moveDescriptor.DstRow - moveDescriptor.SrcRow)
            == Math.Abs(moveDescriptor.DstCol - moveDescriptor.SrcCol);
    }
}
