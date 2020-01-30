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
                    if (IsMoveValidForPiece(move, srcPiece, dstPiece != null))
                    {
                        if (srcPiece is Knight || IsMoveValidForPath(board, move))
                        {
                            if (!IsKingUnderCheck(board, turnColor, move))
                            {
                                return MoveValidationResult.Valid;
                            }

                            return MoveValidationResult.KingUnderCheck;
                        }

                        return MoveValidationResult.InvalidPath;
                    }

                    if (srcPiece is King king)
                    {
                        return ValidateCastling(board, turnColor, move, king);
                    }

                    return MoveValidationResult.InvalidMove;
                }

                return MoveValidationResult.InvalidDst;
            }

            return MoveValidationResult.InvalidSrc;
        }

        public bool IsPositionUnderCheck(Board board, PiecesColor turnColor, Position pos)
        {
            foreach (var (_, srcPos) in board.PiecesPositions[turnColor])
            {
                var move = new Move(srcPos, pos);
                if (Validate(board, turnColor, move) == MoveValidationResult.Valid)
                {
                    return true;
                }
            }

            return false;
        }

        private static bool IsMoveValidForPiece(Move move, Piece srcPiece, bool eating) =>
            srcPiece switch
            {
                Pawn pawn => IsMoveValidForPawn(move, pawn, eating),
                Bishop _ => IsMoveValidForBishop(move),
                Knight _ => IsMoveValidForKnight(move),
                Rook _ => IsMoveValidForRook(move),
                Queen _ => IsMoveValidForQueen(move),
                King _ => IsMoveValidForKing(move),
                _ => throw new NotImplementedException()
            };

        private static bool IsMoveValidForPawn(Move move, Pawn pawn, bool eating)
        {
            var normalisedMove = pawn.Color == PiecesColor.White
                ? move
                : new Move((7, 7) - move.Src, (7, 7) - move.Dst);

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
                tempBoard.KingsPositions[turnColor]);
        }

        private MoveValidationResult ValidateCastling(Board board, PiecesColor turnColor,
            Move move, King king)
        {
            if (Math.Abs(move.Delta.Row) == 0 && Math.Abs(move.Delta.Col) == 2)
            {
                var (incCol, rookCol) = move.Delta.Col > 0
                    ? (1, 7)
                    : (-1, 0);

                var rookPos = new Position(move.Src.Row, rookCol);
                if (board[rookPos] is Rook rook && rook.Color == king.Color)
                {
                    if (!board.CastlingPiecesMoved[king] && !board.CastlingPiecesMoved[rook])
                    {
                        if (IsMoveValidForPath(board, new Move(move.Src, rookPos)))
                        {
                            for (var col = move.Src.Col; col != move.Dst.Col + incCol; col += incCol)
                            {
                                var kingPos = new Position(move.Src.Row, col);
                                if (IsPositionUnderCheck(board, turnColor.Invert(), kingPos))
                                {
                                    return MoveValidationResult.KingUnderCheck;
                                }
                            }

                            return MoveValidationResult.Valid;
                        }

                        return MoveValidationResult.InvalidPath;
                    }

                    return MoveValidationResult.CastlingPiecesAlreadyMoved;
                }

                return MoveValidationResult.CastlingRookNotInPosition;
            }

            return MoveValidationResult.InvalidMove;
        }
    }
}
