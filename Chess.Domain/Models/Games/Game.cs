using Chess.Domain.Models.Players;

namespace Chess.Domain.Models.Games
{
    public abstract class Game
    {
        protected readonly Player Player1;
        protected readonly Player Player2;

        public Board Board { get; }

        protected Game(Player player1, Player player2)
        {
            Player1 = player1;
            Player2 = player2;
            Board = new Board();
        }
    }
}
