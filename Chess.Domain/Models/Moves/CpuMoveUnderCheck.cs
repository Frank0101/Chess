namespace Chess.Domain.Models.Moves
{
    public class CpuMoveUnderCheck : ICpuMove
    {
        public int Value { get; }

        public CpuMoveUnderCheck(int value)
        {
            Value = value;
        }

        public override string ToString() =>
            $"check({Value})";
    }
}
