using System;
using Chess.Domain.Enums;
using Chess.Domain.Factories;
using Chess.Domain.Models.Players;

namespace Chess.Domain.Models.Games
{
    public abstract class Game : IGame
    {
        protected readonly IPlayer Player1;
        protected readonly IPlayer Player2;

        public IBoard Board { get; }

        public event Action<PiecesColor>? TurnStarted;

        protected Game(IBoardFactory boardFactory, IPlayer player1, IPlayer player2)
        {
            if (player1.Color == player2.Color)
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

                if (turnPlayer.TryGetMove(Board, out var move))
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
