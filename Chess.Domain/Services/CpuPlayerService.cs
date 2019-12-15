using System.Collections.Generic;
using System.Linq;
using Chess.Domain.Enums;
using Chess.Domain.Models;
using Chess.Domain.Models.Moves;
using Chess.Domain.Models.Players;
using Chess.Domain.Services.Interfaces;

namespace Chess.Domain.Services
{
    public class CpuPlayerService : ICpuPlayerService
    {
        private readonly IMoveValidationService _moveValidationService;

        public CpuPlayerService(IMoveValidationService moveValidationService)
        {
            _moveValidationService = moveValidationService;
        }

        public Move? GetMove(CpuPlayer player, Board board, PiecesColor turnColor)
        {
            // todo
            return GetBestMove(board, turnColor);
        }

        private CpuMove? GetBestMove(Board board, PiecesColor turnColor)
        {
            var validMoves = GetValidMoves(board, turnColor);

            // todo
            return validMoves.First();
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
                            var move = new CpuMove(srcRow, srcCol, dstRow, dstCol,
                                board[dstRow, dstCol]?.Value ?? 0);

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
