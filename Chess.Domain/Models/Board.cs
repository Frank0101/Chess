using System.Text;
using Chess.Domain.Enums;
using Chess.Domain.Models.Pieces;
using Chess.Domain.services;

namespace Chess.Domain.Models
{
    public class Board
    {
        private readonly IMoveValidationService _moveValidationService;

        internal Board(IMoveValidationService moveValidationService)
        {
            _moveValidationService = moveValidationService;
        }

        private readonly Tile[,] _tiles = new Tile[8, 8];

        public PiecesColor TurnColor => PiecesColor.White;

        public Tile this[int row, int col] => _tiles[row, col];

        public Board()
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

        public MoveValidationResult TryCreateMove(MoveDescriptor moveDescriptor, out Move? move)
        {
            move = null;

            var moveValidationResult = _moveValidationService.ValidateMove(this, moveDescriptor);
            if (moveValidationResult == MoveValidationResult.Valid)
            {
                move = new Move(this, moveDescriptor);
            }

            return moveValidationResult;
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
    }
}
