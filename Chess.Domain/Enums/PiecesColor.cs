using System;

namespace Chess.Domain.Enums
{
    public enum PiecesColor
    {
        Black = 0,
        White = 1
    };

    public static class PiecesColorExtensions
    {
        public static PiecesColor Invert(this PiecesColor color)
        {
            return color switch
            {
                PiecesColor.Black => PiecesColor.White,
                PiecesColor.White => PiecesColor.Black,
                _ => throw new ArgumentOutOfRangeException(nameof(color), color, null)
            };
        }
    }
}
