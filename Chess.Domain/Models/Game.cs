using Chess.Domain.Enums;

namespace Chess.Domain.Models
{
    public class Game
    {
        public Board Board { get; }
        public PiecesColor TurnColor { get; set; }

        public Game()
        {
            Board = new Board();
            TurnColor = PiecesColor.White;
        }
    }
}
