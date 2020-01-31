namespace Chess.Domain.Models.Moves
{
    public class CpuMove : Move, ICpuMoveResponse
    {
        public decimal Value { get; set; }
        public ICpuMoveResponse? Response { get; set; }

        public CpuMove(Position src, Position dst)
            : base(src, dst)
        {
        }

        public override string ToString() =>
            $"{Src}{Dst}({Value})" + (Response != null
                ? $" -> {Response}"
                : "");
    }
}
