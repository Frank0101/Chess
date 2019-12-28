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
            var srcPiece = board[move.Src];
            var dstPiece = board[move.Dst];

            if (srcPiece != null && srcPiece.Color == turnColor)
            {
                if (dstPiece == null || dstPiece.Color != turnColor)
                {
                    if (IsMoveValidForPiece(srcPiece, dstPiece, move))
                    {
                        if (srcPiece is Knight || IsMoveValidForPath(board, move))
                        {
                            return !IsKingUnderCheck(board, turnColor, move)
                                ? MoveValidationResult.Valid
                                : MoveValidationResult.KingUnderCheck;
                        }

                        return MoveValidationResult.InvalidPath;
                    }

                    return MoveValidationResult.InvalidMove;
                }

                return MoveValidationResult.InvalidDst;
            }

            return MoveValidationResult.InvalidSrc;
        }

        public bool IsPositionUnderCheck(Board board, PiecesColor turnColor, Position position)
        {
            for (var srcRow = 0; srcRow < 8; srcRow++)
            {
                for (var srcCol = 0; srcCol < 8; srcCol++)
                {
                    var move = new Move(new Position(srcRow, srcCol), position);
                    if (Validate(board, turnColor, move) == MoveValidationResult.Valid)
                    {
                        return true;
                    }
                }
            }

            return false;
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
                PiecesColor.Black => new Move((7, 7) - move.Src, (7, 7) - move.Dst),
                _ => throw new NotImplementedException()
            };

            if (normalisedMove.Delta.Row > 0)
            {
                if (normalisedMove.Delta.Col == 0 && !eating)
                {
                    if (normalisedMove.Src.Row == 1 && normalisedMove.Delta.Row < 3
                        || normalisedMove.Delta.Row == 1)
                    {
                        return true;
                    }
                }
                else if (normalisedMove.Delta.Col != 0 && eating)
                {
                    if (Math.Abs(normalisedMove.Delta.Col) == 1 && normalisedMove.Delta.Row == 1)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private static bool IsMoveValidForBishop(Move move) =>
            Math.Abs(move.Delta.Row) == Math.Abs(move.Delta.Col);

        private static bool IsMoveValidForKnight(Move move) =>
            Math.Abs(move.Delta.Row) == 1 && Math.Abs(move.Delta.Col) == 2
            || Math.Abs(move.Delta.Row) == 2 && Math.Abs(move.Delta.Col) == 1;

        private static bool IsMoveValidForRook(Move move) =>
            move.Delta.Row == 0 && move.Delta.Col != 0
            || move.Delta.Row != 0 && move.Delta.Col == 0;

        private static bool IsMoveValidForQueen(Move move) =>
            IsMoveValidForBishop(move) || IsMoveValidForRook(move);

        private static bool IsMoveValidForKing(Move move) =>
            Math.Abs(move.Delta.Row) < 2 && Math.Abs(move.Delta.Col) < 2;

        private static bool IsMoveValidForPath(Board board, Move move)
        {
            var (incRow, incCol) = (Math.Sign(move.Delta.Row), Math.Sign(move.Delta.Col));

            if (incCol != 0)
            {
                var row = move.Src.Row + incRow;
                for (var col = move.Src.Col + incCol; col != move.Dst.Col; col += incCol)
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
                for (var row = move.Src.Row + incRow; row != move.Dst.Row; row += incRow)
                {
                    if (board[row, move.Src.Col] != null)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private bool IsKingUnderCheck(Board board, PiecesColor turnColor, Move move)
        {
            var tempBoard = new Board(board);
            tempBoard.ApplyMove(move);

            return IsPositionUnderCheck(tempBoard, turnColor.Invert(),
                tempBoard.KingPositions[turnColor]);
        }
    }
}
