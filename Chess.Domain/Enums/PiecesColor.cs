using System;

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
            color switch
            {
                PiecesColor.Black => PiecesColor.White,
                PiecesColor.White => PiecesColor.Black,
                _ => throw new ArgumentOutOfRangeException()
            };
    }
}
