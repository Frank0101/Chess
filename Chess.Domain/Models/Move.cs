using System;
using Chess.Domain.Models.Pieces;

namespace Chess.Domain.Models
{
    public class Move : IMove
    {
        private readonly IBoard _board;
        private readonly int _turnIndex;
        private IPiece? _eatenPiece;

        public Tile SrcTile { get; }
        public Tile DstTile { get; }

        public Move(IBoard board, MoveDescriptor moveDescriptor)
        {
            _board = board;
            _turnIndex = _board.TurnIndex;

            SrcTile = _board[moveDescriptor.SrcRow, moveDescriptor.SrcCol];
            DstTile = _board[moveDescriptor.DstRow, moveDescriptor.DstCol];
        }

        public void Apply()
        {
            if (_board.TurnIndex == _turnIndex)
            {
                _eatenPiece = DstTile.Piece;
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
            if (_board.TurnIndex == _turnIndex)
            {
                SrcTile.Piece = DstTile.Piece;
                DstTile.Piece = _eatenPiece;
                _eatenPiece = null;
            }
            else
            {
                throw new InvalidOperationException();
            }
        }
    }
}
