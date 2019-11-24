using System.Text;
using Chess.Domain.Enums;
using Chess.Domain.Factories;
using Chess.Domain.Models.Pieces;

namespace Chess.Domain.Models
{
    public class Board : IBoard
    {
        private readonly IPieceFactory _pieceFactory;

        private readonly Tile[,] _tiles = new Tile[8, 8];

        public PiecesColor TurnColor { get; private set; }

        public Tile this[int row, int col] => _tiles[row, col];

        public Board(IPieceFactory pieceFactory)
        {
            _pieceFactory = pieceFactory;

            TurnColor = PiecesColor.White;
            InitPieces();
        }

        public MoveValidationResult TryCreateMove(MoveDescriptor moveDescriptor, out Move? move)
        {
            move = null;

            var moveValidationResult = ValidateMove(moveDescriptor);
            if (moveValidationResult == MoveValidationResult.Valid)
            {
                move = new Move(this, moveDescriptor);
            }

            return moveValidationResult;
        }

        public void ApplyMove(Move move)
        {
            TurnColor = TurnColor.Invert();
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
                        _tiles[row, 0] = new Tile(new Rook(color));
                        _tiles[row, 1] = new Tile(new Knight(color));
                        _tiles[row, 2] = new Tile(new Bishop(color));
                        _tiles[row, 3] = new Tile(new Queen(color));
                        _tiles[row, 4] = new Tile(new King(color));
                        _tiles[row, 5] = new Tile(new Bishop(color));
                        _tiles[row, 6] = new Tile(new Knight(color));
                        _tiles[row, 7] = new Tile(new Rook(color));

                        break;
                    }
                    case 1:
                    case 6:
                    {
                        for (var col = 0; col < 8; col++)
                        {
                            _tiles[row, col] = new Tile(new Pawn(color));
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

            bool IsSrcValid() => srcTile.Piece switch
            {
                {} piece when piece.Color == TurnColor => true,
                _ => false
            };

            bool IsDstValid() => dstTile.Piece switch
            {
                null => true,
                {} piece when piece.Color != TurnColor => true,
                _ => false
            };

            bool IsMoveValid() => srcTile.Piece switch
            {
                {} piece when piece.IsMoveValid(moveDescriptor, false) => true,
                _ => false
            };

            bool IsPathValid() => srcTile.Piece switch
            {
                Knight _ => true,
                _ => false
            };

            if (!IsSrcValid()) return MoveValidationResult.InvalidSrc;
            if (!IsDstValid()) return MoveValidationResult.InvalidDst;
            if (!IsMoveValid()) return MoveValidationResult.InvalidMove;
            if (!IsPathValid()) return MoveValidationResult.InvalidPath;
            return MoveValidationResult.Valid;
        }
    }
}
