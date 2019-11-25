namespace Chess.Domain.Models
{
    public interface IMove
    {
        Tile SrcTile { get; }
        Tile DstTile { get; }

        void Apply();
        void Undo();
    }
}
