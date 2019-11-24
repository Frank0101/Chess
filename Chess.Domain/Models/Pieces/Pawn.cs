using System;
using Chess.Domain.Enums;

namespace Chess.Domain.Models.Pieces
{
    public class Pawn : Piece
    {
        public Pawn(PiecesColor color)
            : base(color, color == PiecesColor.Black ? 'p' : 'P', 1)
        {
        }

        public override bool IsMoveValid(MoveDescriptor moveDescriptor, bool eating)
        {
            var normalisedMovDesc = Color switch
            {
                PiecesColor.Black => moveDescriptor,
                PiecesColor.White => new MoveDescriptor(
                    7 - moveDescriptor.SrcRow, 7 - moveDescriptor.SrcCol,
                    7 - moveDescriptor.DstRow, 7 - moveDescriptor.DstCol),
                _ => throw new NotImplementedException()
            };

            var deltaRow = normalisedMovDesc.DstRow - normalisedMovDesc.SrcRow;
            var deltaCol = normalisedMovDesc.DstCol - normalisedMovDesc.SrcCol;

            if (deltaRow > 0)
            {
                if (deltaCol == 0 && !eating)
                {
                    if (normalisedMovDesc.SrcRow == 1 && deltaRow <= 2 || deltaRow == 1)
                    {
                        return true;
                    }
                }
                else if (deltaCol != 0 && eating)
                {
                    if (Math.Abs(deltaCol) == 1 && deltaRow == 1)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
