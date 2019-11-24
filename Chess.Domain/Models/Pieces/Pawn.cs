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
                PiecesColor.Black => new MoveDescriptor(
                    7 - moveDescriptor.SrcRow, 7 - moveDescriptor.SrcCol,
                    7 - moveDescriptor.DstRow, 7 - moveDescriptor.DstCol),
                PiecesColor.White => moveDescriptor,
                _ => throw new NotImplementedException()
            };

            var deltaRow = normalisedMovDesc.SrcRow - normalisedMovDesc.DstRow;
            var deltaCol = normalisedMovDesc.SrcCol - normalisedMovDesc.DstCol;

            if (deltaRow > 0)
            {
                if (deltaCol == 0 && !eating)
                {
                    if (normalisedMovDesc.SrcRow == 6 && deltaRow <= 2 || deltaRow == 1)
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
