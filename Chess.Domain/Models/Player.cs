using Chess.Domain.Enums;

namespace Chess.Domain.Models
{
    public class Player
    {
        public string Name { get; }
        public PiecesColor Color { get; }

        public Player(string name, PiecesColor color)
        {
            Name = name;
            Color = color;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
