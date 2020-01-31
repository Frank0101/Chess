namespace Chess.Domain.Models.Moves
{
    public class CpuMoveCheck : ICpuMoveResponse
    {
        public decimal Value { get; }

        public CpuMoveCheck(decimal value)
        {
            Value = value;
        }

        public override string ToString() =>
            $"check({Value})";
    }
}
