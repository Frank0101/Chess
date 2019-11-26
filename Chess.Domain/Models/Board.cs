using System.Collections.Generic;
using System.Text;
using Chess.Domain.Enums;
using Chess.Domain.Factories;
using Chess.Domain.Models.Pieces;

namespace Chess.Domain.Models
{
    public class Board : IBoard
    {
        private readonly Tile[,] _tiles = new Tile[8, 8];
        private readonly Queue<IMove> _moves = new Queue<IMove>();

        private readonly IPieceFactory _pieceFactory;
        private readonly IMoveFactory _moveFactory;

        public PiecesColor TurnColor { get; private set; }
        public int TurnIndex { get; private set; }

        public Tile this[int row, int col] => _tiles[row, col];

        public Board(IPieceFactory pieceFactory, IMoveFactory moveFactory)
        {
            _pieceFactory = pieceFactory;
            _moveFactory = moveFactory;

            TurnColor = PiecesColor.White;
            InitPieces();
        }

        public MoveValidationResult TryCreateMove(MoveDescriptor moveDescriptor, out IMove? move)
        {
            move = null;

            var moveValidationResult = ValidateMove(moveDescriptor);
            if (moveValidationResult == MoveValidationResult.Valid)
            {
                move = _moveFactory.Create(this, moveDescriptor);
            }

            return moveValidationResult;
        }

        public void ApplyMove(IMove move)
        {
            move.Apply();

            _moves.Enqueue(move);

            TurnColor = TurnColor.Invert();
            TurnIndex++;
        }

        public IMove UndoLastMove()
        {
            TurnIndex--;
            TurnColor = TurnColor.Invert();

            var move = _moves.Dequeue();

            move.Undo();
            return move;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            for (var row = 0; row < 8; row++)
            {
                if (row > 0) sb.AppendLine();
                for (var col = 0; col < 8; col++)
                {
                    sb.Append(_tiles[row, col]);
                }
            }

            return sb.ToString();
        }

        private void InitPieces()
        {
            for (var row = 0; row < 8; row++)
            {
                var color = row < 4
                    ? PiecesColor.Black
                    : PiecesColor.White;

                switch (row)
                {
                    case 0:
                    case 7:
                    {
                        _tiles[row, 0] = new Tile(_pieceFactory.CreateRook(color));
                        _tiles[row, 1] = new Tile(_pieceFactory.CreateKnight(color));
                        _tiles[row, 2] = new Tile(_pieceFactory.CreateBishop(color));
                        _tiles[row, 3] = new Tile(_pieceFactory.CreateQueen(color));
                        _tiles[row, 4] = new Tile(_pieceFactory.CreateKing(color));
                        _tiles[row, 5] = new Tile(_pieceFactory.CreateBishop(color));
                        _tiles[row, 6] = new Tile(_pieceFactory.CreateKnight(color));
                        _tiles[row, 7] = new Tile(_pieceFactory.CreateRook(color));

                        break;
                    }
                    case 1:
                    case 6:
                    {
                        for (var col = 0; col < 8; col++)
                        {
                            _tiles[row, col] = new Tile(_pieceFactory.CreatePawn(color));
                        }

                        break;
                    }
                    default:
                    {
                        for (var col = 0; col < 8; col++)
                        {
                            _tiles[row, col] = new Tile(null);
                        }

                        break;
                    }
                }
            }
        }

        private MoveValidationResult ValidateMove(MoveDescriptor moveDescriptor)
        {
            var srcTile = _tiles[moveDescriptor.SrcRow, moveDescriptor.SrcCol];
            var dstTile = _tiles[moveDescriptor.DstRow, moveDescriptor.DstCol];

            if (srcTile.Piece != null && srcTile.Piece.Color == TurnColor)
            {
                if (dstTile.Piece == null || dstTile.Piece.Color != TurnColor)
                {
                    var eating = dstTile.Piece != null;
                    if (srcTile.Piece.IsMoveValid(moveDescriptor, eating))
                    {
                        if (srcTile.Piece is Knight || ValidatePath(moveDescriptor))
                        {
                            return MoveValidationResult.Valid;
                        }

                        return MoveValidationResult.InvalidPath;
                    }

                    return MoveValidationResult.InvalidMove;
                }

                return MoveValidationResult.InvalidDst;
            }

            return MoveValidationResult.InvalidSrc;
        }

        private bool ValidatePath(MoveDescriptor moveDescriptor)
        {
            int GetIncrement(int value) => value switch
            {
                var result when result > 0 => 1,
                var result when result < 0 => -1,
                _ => 0
            };

            var incRow = GetIncrement(moveDescriptor.DstRow - moveDescriptor.SrcRow);
            var incCol = GetIncrement(moveDescriptor.DstCol - moveDescriptor.SrcCol);

            if (incCol != 0)
            {
                var row = moveDescriptor.SrcRow + incRow;
                for (var col = moveDescriptor.SrcCol + incCol; col != moveDescriptor.DstCol; col += incCol)
                {
                    if (_tiles[row, col].Piece != null)
                    {
                        return false;
                    }

                    row += incRow;
                }
            }
            else
            {
                var col = moveDescriptor.SrcCol;
                for (var row = moveDescriptor.SrcRow + incRow; row != moveDescriptor.DstRow; row += incRow)
                {
                    if (_tiles[row, col].Piece != null)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
