namespace Chess.Domain.Enums
{
    public enum MoveValidationResult
    {
        InvalidSrc,
        InvalidDst,
        InvalidMove,
        InvalidPath,
        KingUnderCheck,
        Valid
    }
}
