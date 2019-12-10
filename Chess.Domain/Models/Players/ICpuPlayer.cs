namespace Chess.Domain.Models.Players
{
    public interface ICpuPlayer : IPlayer
    {
        int RecursionLevel { get; }
    }
}
