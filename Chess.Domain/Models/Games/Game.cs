using System;
using Chess.Domain.Enums;
using Chess.Domain.Models.Players;

namespace Chess.Domain.Models.Games
{
    public abstract class Game
    {
        protected readonly Player Player1;
        protected readonly Player Player2;

        public Board Board { get; }

        public event Action<PiecesColor>? TurnStarted;

        protected Game(Player player1, Player player2)
        {
            if (player1.Color != player2.Color.Invert())
                throw new ArgumentException();

            Player1 = player1;
            Player2 = player2;
            Board = new Board();
        }

        public void Start()
        {
            var turnColor = PiecesColor.White;

            while (true)
            {
                TurnStarted?.Invoke(turnColor);

                var turnPlayer = turnColor == Player1.Color
                    ? Player1
                    : Player2;

                if (turnPlayer.TryMove(Board))
                {
                    turnColor = turnColor.Invert();
                }
                else
                {
                    break;
                }
            }
        }
    }
}
