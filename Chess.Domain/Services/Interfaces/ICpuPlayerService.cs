using Chess.Domain.Models.Players;

namespace Chess.Domain.Services.Interfaces
{
    public interface ICpuPlayerService : IPlayerService<CpuPlayer>
    {
    }
}
