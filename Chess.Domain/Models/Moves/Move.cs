namespace Chess.Domain.Models.Moves
{
    public class Move
    {
        public Position Src { get; }
        public Position Dst { get; }
        public Position Delta { get; }
        public Move? Next { get; set; }

        public Move(Position src, Position dst)
        {
            Src = src;
            Dst = dst;
            Delta = dst - src;
        }

        public Move(int srcRow, int srcCol, int dstRow, int dstCol)
            : this(
                new Position(srcRow, srcCol),
                new Position(dstRow, dstCol))
        {
        }

        public override string ToString() =>
            $"{Src}{Dst}" + (Next != null
                ? $"+{Next}"
                : "");
    }
}
