using System;
using Chess.Domain.Models.Pieces;

namespace Chess.Domain.Models
{
    public class Move : IMove
    {
        public IBoard Board { get; }
        public int TurnIndex { get; private set; }
        public Tile SrcTile { get; }
        public Tile DstTile { get; }
        public IPiece? EatenPiece { get; private set; }

        public Move(IBoard board, MoveDescriptor moveDescriptor)
        {
            Board = board;
            TurnIndex = Board.TurnIndex;

            SrcTile = Board[moveDescriptor.SrcRow, moveDescriptor.SrcCol];
            DstTile = Board[moveDescriptor.DstRow, moveDescriptor.DstCol];
        }

        public void Apply()
        {
            if (Board.TurnIndex == TurnIndex)
            {
                EatenPiece = DstTile.Piece;
                DstTile.Piece = SrcTile.Piece;
                SrcTile.Piece = null;
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public void Undo()
        {
            if (Board.TurnIndex == TurnIndex)
            {
                SrcTile.Piece = DstTile.Piece;
                DstTile.Piece = EatenPiece;
                EatenPiece = null;
            }
            else
            {
                throw new InvalidOperationException();
            }
        }
    }
}
