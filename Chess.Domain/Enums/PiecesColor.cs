namespace Chess.Domain.Enums
{
    public enum PiecesColor
    {
        White,
        Black
    };

    public static class PiecesColorExtensions
    {
        public static PiecesColor Invert(this PiecesColor color) =>
            color == PiecesColor.White
                ? PiecesColor.Black
                : PiecesColor.White;
    }
}
