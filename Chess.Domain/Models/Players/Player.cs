using Chess.Domain.Enums;

namespace Chess.Domain.Models.Players
{
    public abstract class Player
    {
        public PiecesColor Color { get; }

        protected Player(PiecesColor color)
        {
            Color = color;
        }

        public abstract bool TryMove(Board board);
    }
}
