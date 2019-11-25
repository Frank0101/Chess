using Chess.Domain.Models.Pieces;

namespace Chess.Domain.Models
{
    public interface IMove
    {
        IBoard Board { get; }
        int TurnIndex { get; }
        Tile SrcTile { get; }
        Tile DstTile { get; }
        IPiece? EatenPiece { get; }

        void Apply();
        void Undo();
    }
}
