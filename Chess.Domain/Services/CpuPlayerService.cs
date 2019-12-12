using Chess.Domain.Enums;
using Chess.Domain.Models;
using Chess.Domain.Models.Players;
using Chess.Domain.Services.Interfaces;

namespace Chess.Domain.Services
{
    public class CpuPlayerService : ICpuPlayerService
    {
        public Move? GetMove(CpuPlayer player, Board board, PiecesColor turnColor)
        {
            // todo
            return null;
        }
    }
}
