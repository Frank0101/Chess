using Chess.Domain.Enums;
using Chess.Domain.Models;
using Chess.Domain.Models.Players;

namespace Chess.Domain.Services.Interfaces
{
    public interface IPlayerService<in T> where T : IPlayer
    {
        bool TryGetMove(T player, Board board, PiecesColor turnColor, out Move? move);
    }
}
