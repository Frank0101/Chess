using System;
using System.Collections.Generic;
using System.Linq;
using Chess.Domain.Enums;
using Chess.Domain.Models;
using Chess.Domain.Models.Moves;
using Chess.Domain.Models.Pieces;
using Chess.Domain.Services.Interfaces;

namespace Chess.Domain.Services
{
    public class MoveEvaluationService : IMoveEvaluationService
    {
        private readonly IMoveValidationService _moveValidationService;

        public MoveEvaluationService(
            IMoveValidationService moveValidationService)
        {
            _moveValidationService = moveValidationService;
        }

        public List<CpuMove> GetValidCpuMoves(Board board, PiecesColor turnColor)
        {
            var validMoves = new List<CpuMove>();

            foreach (var (piece, srcPos) in board.PiecesPositions[turnColor])
            {
                foreach (var move in GetPotentialDestinations(piece, srcPos)
                    .Select(dstPos => new CpuMove(srcPos, dstPos))
                    .Where(move =>
                        _moveValidationService.Validate(board, turnColor, move)
                        == MoveValidationResult.Valid))
                {
                    move.Value = EvaluateMoveValue(board, move);
                    validMoves.Add(move);
                }
            }

            return validMoves;
        }

        private static List<Position> GetPotentialDestinations(Piece piece, Position srcPos) =>
            (piece switch
            {
                Pawn pawn => GetPotentialDestinationsForPawn(pawn, srcPos),
                Bishop _ => GetPotentialDestinationsForBishop(srcPos),
                Knight _ => GetPotentialDestinationsForKnight(srcPos),
                Rook _ => GetPotentialDestinationsForRook(srcPos),
                Queen _ => GetPotentialDestinationsForQueen(srcPos),
                King _ => GetPotentialDestinationsForKing(srcPos),
                _ => throw new NotImplementedException()
            }).Where(pos => pos.IsInRange).ToList();

        private static IEnumerable<Position> GetPotentialDestinationsForPawn(Pawn pawn, Position srcPos)
        {
            var incRow = pawn.Color == PiecesColor.White ? 1 : -1;

            return new List<Position>
            {
                (incRow, 0) + srcPos, (incRow * 2, 0) + srcPos,
                (incRow, -1) + srcPos, (incRow, 1) + srcPos
            };
        }

        private static IEnumerable<Position> GetPotentialDestinationsForBishop(Position srcPos) =>
            Enumerable.Range(1, 7)
                .SelectMany(inc => new List<Position>
                {
                    (inc, -inc) + srcPos,
                    (inc, inc) + srcPos,
                    (-inc, -inc) + srcPos,
                    (-inc, inc) + srcPos
                });

        private static IEnumerable<Position> GetPotentialDestinationsForKnight(Position srcPos) =>
            new List<Position>
            {
                (2, -1) + srcPos, (2, 1) + srcPos,
                (-2, -1) + srcPos, (-2, 1) + srcPos,
                (1, -2) + srcPos, (-1, -2) + srcPos,
                (1, 2) + srcPos, (-1, 2) + srcPos
            };

        private static IEnumerable<Position> GetPotentialDestinationsForRook(Position srcPos) =>
            Enumerable.Range(1, 7)
                .SelectMany(inc => new List<Position>
                {
                    (inc, 0) + srcPos,
                    (-inc, 0) + srcPos,
                    (0, -inc) + srcPos,
                    (0, inc) + srcPos
                });

        private static IEnumerable<Position> GetPotentialDestinationsForQueen(Position srcPos) =>
            GetPotentialDestinationsForBishop(srcPos).Concat(GetPotentialDestinationsForRook(srcPos));

        private static IEnumerable<Position> GetPotentialDestinationsForKing(Position srcPos) =>
            new List<Position>
            {
                (1, -1) + srcPos, (1, 0) + srcPos, (1, 1) + srcPos,
                (0, -1) + srcPos, (0, 1) + srcPos,
                (-1, -1) + srcPos, (-1, 0) + srcPos, (-1, 1) + srcPos,
                (0, -2) + srcPos, (0, 2) + srcPos,
            };

        private static decimal EvaluateMoveValue(Board board, Move move)
        {
            var moveValue = (decimal) (board[move.Dst]?.Value ?? 0);

            if (board[move.Src] is King)
            {
                moveValue -= 0.1m;
            }

            return moveValue;
        }
    }
}
