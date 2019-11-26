using Chess.Domain.Enums;

namespace Chess.Domain.Models.Players
{
    public abstract class Player : IPlayer
    {
        public PiecesColor Color { get; }

        protected Player(PiecesColor color)
        {
            Color = color;
        }

        public abstract bool TryGetMove(IBoard board, out IMove move);
    }
}
