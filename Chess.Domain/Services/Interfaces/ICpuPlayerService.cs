using Chess.Domain.Enums;
using Chess.Domain.Models;
using Chess.Domain.Models.Players;

namespace Chess.Domain.Services.Interfaces
{
    public interface ICpuPlayerService
    {
        bool TryGetMove(CpuPlayer player, Board board, PiecesColor turnColor, out Move? move);
    }
}
