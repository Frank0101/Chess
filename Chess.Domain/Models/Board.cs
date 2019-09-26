using Chess.Domain.Models.Pieces;

namespace Chess.Domain.Models
{
    public class Board
    {
        private readonly Tile[,] _tiles = new Tile[8, 8];

        public Tile this[int row, int col] => _tiles[row, col];

        public Board(PiecesColor userColor)
        {
            for (var row = 0; row < 8; row++)
            {
                var color = row < 4
                    ? userColor.Invert()
                    : userColor;

                switch (row)
                {
                    case 0:
                    case 7:
                    {
                        _tiles[row, 0] = new Tile(new Rook(color));
                        _tiles[row, 1] = new Tile(new Knight(color));
                        _tiles[row, 2] = new Tile(new Bishop(color));

                        if (userColor == PiecesColor.White)
                        {
                            _tiles[row, 3] = new Tile(new Queen(color));
                            _tiles[row, 4] = new Tile(new King(color));
                        }
                        else
                        {
                            _tiles[row, 3] = new Tile(new King(color));
                            _tiles[row, 4] = new Tile(new Queen(color));
                        }

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
    }
}
