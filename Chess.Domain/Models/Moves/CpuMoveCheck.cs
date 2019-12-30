namespace Chess.Domain.Models.Moves
{
    public class CpuMoveCheck : ICpuMoveResponse
    {
        public int Value { get; }

        public CpuMoveCheck(int value)
        {
            Value = value;
        }

        public override string ToString() =>
            $"check({Value})";
    }
}
