using Chess.Domain.Enums;
using Chess.Domain.Models;
using Chess.Domain.Models.Pieces;

namespace Chess.Domain.Services
{
    public class MoveValidationService : IMoveValidationService
    {
        public MoveValidationResult ValidateMove(Board board, MoveDescriptor moveDescriptor)
        {
            var srcTile = board[moveDescriptor.SrcRow, moveDescriptor.SrcCol];
            var dstTile = board[moveDescriptor.DstRow, moveDescriptor.DstCol];

            bool IsSrcValid() => srcTile.Piece switch
            {
                {} piece when piece.Color == board.TurnColor => true,
                _ => false
            };

            bool IsDstValid() => dstTile.Piece switch
            {
                null => true,
                {} piece when piece.Color != board.TurnColor => true,
                _ => false
            };

            bool IsMoveValid() => srcTile.Piece switch
            {
                {} piece when piece.IsMoveValid(moveDescriptor, false) => true,
                _ => false
            };

            bool IsPathValid() => srcTile.Piece switch
            {
                Knight _ => true,
                _ => false
            };

            if (!IsSrcValid()) return MoveValidationResult.InvalidSrc;
            if (!IsDstValid()) return MoveValidationResult.InvalidDst;
            if (!IsMoveValid()) return MoveValidationResult.InvalidMove;
            if (!IsPathValid()) return MoveValidationResult.InvalidPath;
            return MoveValidationResult.Valid;
        }
    }
}
