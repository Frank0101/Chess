using System;
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
        private const int MaxNotifiableLevel = 1;
        private readonly Random _random = new Random();

        private readonly IMoveValidationService _moveValidationService;
        private readonly IMoveEvaluationService _moveEvaluationService;

        public CpuPlayerService(
            IMoveValidationService moveValidationService,
            IMoveEvaluationService moveEvaluationService)
        {
            _moveValidationService = moveValidationService;
            _moveEvaluationService = moveEvaluationService;
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
                    var response = GetBestMove(player, tempBoard, turnColor.Invert(),
                        recursionLevel + 1, recursionDepth);

                    move.Response = response ?? (ICpuMoveResponse?) new CpuMoveCheckMate();
                }
                else if (_moveValidationService.IsPositionUnderCheck(tempBoard,
                    turnColor.Invert(), move.Dst))
                {
                    move.Response = new CpuMoveCheck(tempBoard[move.Dst]?.Value ?? 0);
                }
                else
                {
                    move.Response = new CpuMoveUnknown();
                }

                move.Value -= move.Response?.Value ?? 0;
                if (recursionLevel == 0)
                {
                    move.Value += EvaluateBoardCoverageValue(tempBoard, turnColor);
                }
            }

            var startTime = DateTime.Now;
            var moves = _moveEvaluationService.GetValidCpuMoves(board, turnColor);
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

        private decimal EvaluateBoardCoverageValue(Board board, PiecesColor turnColor)
        {
            var coverage = _moveEvaluationService.GetValidCpuMoves(board, turnColor);
            return Math.Round(0.01m * coverage.Count, 1);
        }
    }
}
