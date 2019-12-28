using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chess.Domain.Enums;
using Chess.Domain.Models;
using Chess.Domain.Models.Moves;
using Chess.Domain.Models.Players;
using Chess.Domain.Services.Interfaces;

namespace Chess.Domain.Services
{
    public class CpuPlayerService : ICpuPlayerService
    {
        private const int ChessMateValue = 99;
        private const int MaxNotifiableLevel = 1;
        private readonly Random _random = new Random();

        private readonly IMoveValidationService _moveValidationService;

        public CpuPlayerService(
            IMoveValidationService moveValidationService)
        {
            _moveValidationService = moveValidationService;
        }

        public Move? GetMove(CpuPlayer player, Board board, PiecesColor turnColor) =>
            GetBestMove(player, board, turnColor, 0, player.RecursionDepth);

        private CpuMove? GetBestMove(CpuPlayer player, Board board, PiecesColor turnColor,
            int recursionLevel, int recursionDepth)
        {
            void EvaluateMove(CpuMove move)
            {
                var tempBoard = new Board(board);
                tempBoard.ApplyMove(move);

                if (recursionLevel < recursionDepth)
                {
                    move.Response = GetBestMove(player, tempBoard, turnColor.Invert(),
                        recursionLevel + 1, recursionDepth);

                    if (move.Response != null)
                    {
                        move.Value -= move.Response.Value;
                    }
                    else
                    {
                        move.Value += ChessMateValue;
                    }
                }
                else if (_moveValidationService.IsPositionUnderCheck(tempBoard,
                    turnColor.Invert(), move.Dst))
                {
                    move.Value -= tempBoard[move.Dst]?.Value ?? 0;
                }
            }

            var startTime = DateTime.Now;
            var moves = GetValidMoves(board, turnColor);
            if (!moves.Any()) return null;

            if (recursionLevel == 0)
            {
                Parallel.ForEach(moves, EvaluateMove);
            }
            else
            {
                foreach (var move in moves)
                {
                    EvaluateMove(move);
                }
            }

            var bestValue = moves.Max(move => move.Value);
            var bestMoves = moves.Where(move => move.Value == bestValue).ToArray();
            var bestMove = bestMoves[_random.Next(bestMoves.Length)];

            if (recursionLevel <= MaxNotifiableLevel)
            {
                player.OnMoveEvaluated(recursionLevel,
                    moves, bestMove, DateTime.Now - startTime);
            }

            return bestMove;
        }

        private List<CpuMove> GetValidMoves(Board board, PiecesColor turnColor)
        {
            var validMoves = new List<CpuMove>();

            for (var srcRow = 0; srcRow < 8; srcRow++)
            {
                for (var srcCol = 0; srcCol < 8; srcCol++)
                {
                    for (var dstRow = 0; dstRow < 8; dstRow++)
                    {
                        for (var dstCol = 0; dstCol < 8; dstCol++)
                        {
                            var move = new CpuMove(srcRow, srcCol, dstRow, dstCol)
                            {
                                Value = board[dstRow, dstCol]?.Value ?? 0
                            };

                            if (_moveValidationService.Validate(board, turnColor, move)
                                == MoveValidationResult.Valid)
                            {
                                validMoves.Add(move);
                            }
                        }
                    }
                }
            }

            return validMoves;
        }
    }
}
