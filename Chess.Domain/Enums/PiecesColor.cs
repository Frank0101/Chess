namespace Chess.Domain.Enums
{
    public enum PiecesColor
    {
        Black,
        White
    };

    public static class PiecesColorExtensions
    {
        public static PiecesColor Invert(this PiecesColor color) =>
            color == PiecesColor.Black
                ? PiecesColor.White
                : PiecesColor.Black;
    }
}
