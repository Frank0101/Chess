using System;
using Chess.Domain.Enums;
using Chess.Domain.Models.Players;
using Chess.Domain.Services;

namespace Chess.Domain.Models.Games
{
    public abstract class Game
    {
        protected readonly Player Player1;
        protected readonly Player Player2;

        public Board Board { get; }

        public event Action<PiecesColor>? TurnStarted;

        protected Game(IBoardFactory boardFactory, Player player1, Player player2)
        {
            if (player1.Color != player2.Color.Invert())
                throw new ArgumentException();

            Player1 = player1;
            Player2 = player2;

            Board = boardFactory.Create();
        }

        public void Start()
        {
            while (true)
            {
                TurnStarted?.Invoke(Board.TurnColor);

                var turnPlayer = Board.TurnColor == Player1.Color
                    ? Player1
                    : Player2;

                if (turnPlayer.TryMove(Board))
                {
                }
                else
                {
                    break;
                }
            }
        }
    }
}
