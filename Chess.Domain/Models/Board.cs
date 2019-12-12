using System.Text;
using Chess.Domain.Enums;
using Chess.Domain.Models.Pieces;

namespace Chess.Domain.Models
{
    public class Board
    {
        private readonly Piece?[,] _pieces = new Piece?[8, 8];

        public Piece? this[int row, int col]
        {
            get => _pieces[row, col];
            set => _pieces[row, col] = value;
        }

        public Board()
        {
            for (var row = 0; row < 8; row++)
            {
                var color = row < 4
                    ? PiecesColor.White
                    : PiecesColor.Black;

                switch (row)
                {
                    case 0:
                    case 7:
                    {
                        _pieces[row, 0] = new Rook(color);
                        _pieces[row, 1] = new Knight(color);
                        _pieces[row, 2] = new Bishop(color);
                        _pieces[row, 3] = new Queen(color);
                        _pieces[row, 4] = new King(color);
                        _pieces[row, 5] = new Bishop(color);
                        _pieces[row, 6] = new Knight(color);
                        _pieces[row, 7] = new Rook(color);

                        break;
                    }
                    case 1:
                    case 6:
                    {
                        for (var col = 0; col < 8; col++)
                        {
                            _pieces[row, col] = new Pawn(color);
                        }

                        break;
                    }
                    default:
                    {
                        for (var col = 0; col < 8; col++)
                        {
                            _pieces[row, col] = null;
                        }

                        break;
                    }
                }
            }
        }

        public Board(Board board)
        {
            for (var row = 0; row < 8; row++)
            {
                for (var col = 0; col < 8; col++)
                {
                    _pieces[row, col] = board[row, col];
                }
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            for (var row = 7; row >= 0; row--)
            {
                sb.Append($"{row + 1} ");

                for (var col = 0; col < 8; col++)
                {
                    sb.Append($" {_pieces[row, col]?.ToString() ?? " "} ");
                }

                sb.AppendLine();
            }

            sb.Append("   a  b  c  d  e  f  g  h");

            return sb.ToString();
        }
    }
}
