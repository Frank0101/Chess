using Chess.Domain.Enums;

namespace Chess.Domain.Models.Pieces
{
    public abstract class Piece
    {
        public PiecesColor Color { get; }
        public char Symbol { get; }
        public int Value { get; }

        protected Piece(PiecesColor color, char symbol, int value)
        {
            Color = color;
            Symbol = symbol;
            Value = value;
        }

        public override string ToString()
        {
            return Symbol.ToString();
        }
    }
}
