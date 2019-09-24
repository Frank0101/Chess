using System;

namespace Chess.Models
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
            switch (color)
            {
                case PiecesColor.Black: return PiecesColor.White;
                case PiecesColor.White: return PiecesColor.Black;
                default:
                    throw new ArgumentOutOfRangeException
                        (nameof(color), color, null);
            }
        }
    }
}
