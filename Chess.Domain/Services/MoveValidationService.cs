using System;
using Chess.Domain.Enums;
using Chess.Domain.Models;
using Chess.Domain.Models.Moves;
using Chess.Domain.Models.Pieces;
using Chess.Domain.Services.Interfaces;

namespace Chess.Domain.Services
{
    public class MoveValidationService : IMoveValidationService
    {
        public MoveValidationResult Validate(Board board, PiecesColor turnColor, Move move)
        {
            var srcPiece = board[move.SrcRow, move.SrcCol];
            var dstPiece = board[move.DstRow, move.DstCol];

            if (srcPiece != null && srcPiece.Color == turnColor)
            {
                if (dstPiece == null || dstPiece.Color != turnColor)
                {
                    if (IsMoveValidForPiece(srcPiece, dstPiece, move))
                    {
                        return IsMoveValidForPath(board, move)
                            ? MoveValidationResult.Valid
                            : MoveValidationResult.InvalidPath;
                    }

                    return MoveValidationResult.InvalidMove;
                }

                return MoveValidationResult.InvalidDst;
            }

            return MoveValidationResult.InvalidSrc;
        }

        private static bool IsMoveValidForPiece(Piece srcPiece, Piece? dstPiece, Move move) =>
            srcPiece switch
            {
                Pawn pawn => IsMoveValidForPawn(pawn, move, dstPiece != null),
                Bishop _ => IsMoveValidForBishop(move),
                Knight _ => IsMoveValidForKnight(move),
                Rook _ => IsMoveValidForRook(move),
                Queen _ => IsMoveValidForQueen(move),
                King _ => IsMoveValidForKing(move),
                _ => throw new NotImplementedException()
            };

        private static bool IsMoveValidForPawn(Pawn pawn, Move move, bool eating)
        {
            var normalisedMove = pawn.Color switch
            {
                PiecesColor.White => move,
                PiecesColor.Black => new Move(
                    7 - move.SrcRow, 7 - move.SrcCol,
                    7 - move.DstRow, 7 - move.DstCol),
                _ => throw new NotImplementedException()
            };

            if (normalisedMove.DeltaRow > 0)
            {
                if (normalisedMove.DeltaCol == 0 && !eating)
                {
                    if (normalisedMove.SrcRow == 1 && normalisedMove.DeltaRow < 3
                        || normalisedMove.DeltaRow == 1)
                    {
                        return true;
                    }
                }
                else if (normalisedMove.DeltaCol != 0 && eating)
                {
                    if (Math.Abs(normalisedMove.DeltaCol) == 1 && normalisedMove.DeltaRow == 1)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private static bool IsMoveValidForBishop(Move move) =>
            Math.Abs(move.DeltaRow) == Math.Abs(move.DeltaCol);

        private static bool IsMoveValidForKnight(Move move) =>
            Math.Abs(move.DeltaRow) == 1 && Math.Abs(move.DeltaCol) == 2
            || Math.Abs(move.DeltaRow) == 2 && Math.Abs(move.DeltaCol) == 1;

        private static bool IsMoveValidForRook(Move move) =>
            move.DeltaRow == 0 && move.DeltaCol != 0
            || move.DeltaRow != 0 && move.DeltaCol == 0;

        private static bool IsMoveValidForQueen(Move move) =>
            IsMoveValidForBishop(move) || IsMoveValidForRook(move);

        private static bool IsMoveValidForKing(Move move) =>
            Math.Abs(move.DeltaRow) < 2 && Math.Abs(move.DeltaCol) < 2;

        private static bool IsMoveValidForPath(Board board, Move move)
        {
            var (incRow, incCol) = (Math.Sign(move.DeltaRow), Math.Sign(move.DeltaCol));

            if (incCol != 0)
            {
                var row = move.SrcRow + incRow;
                for (var col = move.SrcCol + incCol; col != move.DstCol; col += incCol)
                {
                    if (board[row, col] != null)
                    {
                        return false;
                    }

                    row += incRow;
                }
            }
            else
            {
                for (var row = move.SrcRow + incRow; row != move.DstRow; row += incRow)
                {
                    if (board[row, move.SrcCol] != null)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
