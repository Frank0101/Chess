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
        private const int MaxDegreeOfParallelism = 8;
        private readonly ParallelOptions _parallelOptions;
        private readonly Random _random = new Random();

        private readonly IMoveValidationService _moveValidationService;
        private readonly IMoveExecutionService _moveExecutionService;

        public CpuPlayerService(
            IMoveValidationService moveValidationService,
            IMoveExecutionService moveExecutionService)
        {
            _moveValidationService = moveValidationService;
            _moveExecutionService = moveExecutionService;

            _parallelOptions = new ParallelOptions
            {
                MaxDegreeOfParallelism = MaxDegreeOfParallelism
            };
        }

        public Move? GetMove(CpuPlayer player, Board board, PiecesColor turnColor)
        {
            // todo
            return GetBestMove(board, turnColor, player.RecursionLevel);
        }

        private CpuMove? GetBestMove(Board board, PiecesColor turnColor, int level)
        {
            const int chessMateValue = 99;

            var moves = GetValidMoves(board, turnColor);
            Parallel.ForEach(moves, _parallelOptions, (move) =>
            {
                var tempBoard = new Board(board);
                _moveExecutionService.Execute(tempBoard, move);

                if (level > 0)
                {
                    move.Response = GetBestMove(tempBoard, turnColor.Invert(), level - 1);
                    if (move.Response != null)
                    {
                        move.Value -= move.Response.Value;
                    }
                    else
                    {
                        move.Value += chessMateValue;
                    }
                }
            });

            var bestValue = moves.Max(move => move.Value);
            var bestMoves = moves.Where(move => move.Value == bestValue).ToArray();
            return bestMoves[_random.Next(bestMoves.Length)];
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
