using Chess.Domain.Enums;
using Chess.Domain.Models.Players;

namespace Chess.Domain.Models
{
    public class Game
    {
        public Board Board { get; }
        public IPlayer WhitePlayer { get; }
        public IPlayer BlackPlayer { get; }
        public PiecesColor TurnColor { get; set; }

        public Game(IPlayer whitePlayer, IPlayer blackPlayer)
        {
            Board = new Board();
            WhitePlayer = whitePlayer;
            BlackPlayer = blackPlayer;
            TurnColor = PiecesColor.White;
        }
    }
}
