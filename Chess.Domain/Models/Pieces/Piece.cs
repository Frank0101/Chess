namespace Chess.Domain.Models.Pieces
{
    public abstract class Piece
    {
        public char Symbol { get; }
        public int Value { get; }
        public PiecesColor Color { get; }

        protected Piece(char symbol, int value, PiecesColor color)
        {
            Symbol = symbol;
            Value = value;
            Color = color;
        }

        public override string ToString()
        {
            return Symbol.ToString();
        }
    }
}
