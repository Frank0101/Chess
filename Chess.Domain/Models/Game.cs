using System;
using Chess.Domain.Enums;
using Chess.Domain.Models.Players;

namespace Chess.Domain.Models
{
    public class Game
    {
        public Board Board { get; }
        public IPlayer WhitePlayer { get; }
        public IPlayer BlackPlayer { get; }
        public Action<Board, PiecesColor> OnNewTurn { get; }
        public Func<Board, PiecesColor, Move, bool> OnMoveConfirm { get; }
        public PiecesColor TurnColor { get; set; }

        public Game(IPlayer whitePlayer, IPlayer blackPlayer,
            Action<Board, PiecesColor> onNewTurn, Func<Board, PiecesColor, Move, bool> onMoveConfirm)
        {
            Board = new Board();
            WhitePlayer = whitePlayer;
            BlackPlayer = blackPlayer;
            OnNewTurn = onNewTurn;
            OnMoveConfirm = onMoveConfirm;
            TurnColor = PiecesColor.White;
        }
    }
}
