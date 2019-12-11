using System;
using Chess.Domain.Enums;
using Chess.Domain.Models.Players;

namespace Chess.Domain.Models
{
    public class Game
    {
        public Board Board { get; }
        public PiecesColor TurnColor { get; set; }
        public IPlayer WhitePlayer { get; }
        public IPlayer BlackPlayer { get; }
        public Action<Board, PiecesColor> OnNewTurn { get; }

        public Game(IPlayer whitePlayer, IPlayer blackPlayer,
            Action<Board, PiecesColor> onNewTurn)
        {
            Board = new Board();
            TurnColor = PiecesColor.White;
            WhitePlayer = whitePlayer;
            BlackPlayer = blackPlayer;
            OnNewTurn = onNewTurn;
        }
    }
}
