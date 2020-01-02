using System;

namespace Chess.Domain.Models
{
    public struct Position
    {
        public int Row { get; }
        public int Col { get; }

        public bool IsInRange =>
            Row >= 0 && Row < 8 && Col >= 0 && Col < 8;

        public Position(int row, int col)
        {
            Row = row;
            Col = col;
        }

        public override string ToString() =>
            $"{(char) ('a' + Col)}{Row + 1}";

        public override bool Equals(object? obj) =>
            obj switch
            {
                Position pos when pos == this => true,
                _ => false
            };

        public override int GetHashCode() =>
            HashCode.Combine(Row, Col);

        public static bool operator ==(Position pos1, Position pos2) =>
            pos1.Row == pos2.Row && pos1.Col == pos2.Col;

        public static bool operator !=(Position pos1, Position pos2) =>
            pos1.Row != pos2.Row || pos1.Col != pos2.Col;

        public static bool operator ==((int row, int col) pos1, Position pos2) =>
            pos1.row == pos2.Row && pos1.col == pos2.Col;

        public static bool operator !=((int row, int col) pos1, Position pos2) =>
            pos1.row != pos2.Row || pos1.col != pos2.Col;

        public static Position operator +(Position pos1, Position pos2) =>
            new Position(pos1.Row + pos2.Row, pos1.Col + pos2.Col);

        public static Position operator -(Position pos1, Position pos2) =>
            new Position(pos1.Row - pos2.Row, pos1.Col - pos2.Col);

        public static Position operator +((int row, int col) pos1, Position pos2) =>
            new Position(pos1.row + pos2.Row, pos1.col + pos2.Col);

        public static Position operator -((int row, int col) pos1, Position pos2) =>
            new Position(pos1.row - pos2.Row, pos1.col - pos2.Col);
    }
}
