using System.Collections.Generic;
using System.Text;
using Chess.Domain.Enums;
using Chess.Domain.Models.Moves;
using Chess.Domain.Models.Pieces;

namespace Chess.Domain.Models
{
    public class Board
    {
        private readonly Piece?[,] _pieces = new Piece?[8, 8];

        public Piece? this[int row, int col] => _pieces[row, col];

        public Piece? this[Position pos]
        {
            get => _pieces[pos.Row, pos.Col];
            private set => _pieces[pos.Row, pos.Col] = value;
        }

        public Dictionary<PiecesColor, Position> KingPositions { get; }
        public Dictionary<PiecesColor, Dictionary<Piece, Position>> PiecesPositions { get; }
        public Dictionary<CastlingPiece, bool> CastlingPiecesTouched { get; }

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

            KingPositions = new Dictionary<PiecesColor, Position>
            {
                {PiecesColor.White, new Position(0, 4)},
                {PiecesColor.Black, new Position(7, 4)}
            };

            PiecesPositions = new Dictionary<PiecesColor, Dictionary<Piece, Position>>
            {
                {PiecesColor.White, new Dictionary<Piece, Position>()},
                {PiecesColor.Black, new Dictionary<Piece, Position>()}
            };

            CastlingPiecesTouched = new Dictionary<CastlingPiece, bool>();

            for (var row = 0; row < 8; row++)
            {
                for (var col = 0; col < 8; col++)
                {
                    var piece = _pieces[row, col];
                    if (piece != null)
                    {
                        PiecesPositions[piece.Color].Add(piece, new Position(row, col));

                        if (piece is CastlingPiece castlingPiece)
                        {
                            CastlingPiecesTouched.Add(castlingPiece, false);
                        }
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

            KingPositions = new Dictionary<PiecesColor, Position>
            {
                {PiecesColor.White, board.KingPositions[PiecesColor.White]},
                {PiecesColor.Black, board.KingPositions[PiecesColor.Black]}
            };

            PiecesPositions = new Dictionary<PiecesColor, Dictionary<Piece, Position>>
            {
                {PiecesColor.White, new Dictionary<Piece, Position>()},
                {PiecesColor.Black, new Dictionary<Piece, Position>()}
            };

            foreach (var (piece, pos) in board.PiecesPositions[PiecesColor.White])
            {
                PiecesPositions[PiecesColor.White].Add(piece, pos);
            }

            foreach (var (piece, pos) in board.PiecesPositions[PiecesColor.Black])
            {
                PiecesPositions[PiecesColor.Black].Add(piece, pos);
            }

            CastlingPiecesTouched = new Dictionary<CastlingPiece, bool>();

            foreach (var (castlingPiece, touched) in CastlingPiecesTouched)
            {
                CastlingPiecesTouched.Add(castlingPiece, touched);
            }
        }

        public void ApplyMove(Move move)
        {
            var srcPiece = this[move.Src];
            var dstPiece = this[move.Dst];

            if (srcPiece != null)
            {
                if (srcPiece is King king)
                {
                    KingPositions[king.Color] = move.Dst;
                }

                PiecesPositions[srcPiece.Color][srcPiece] = move.Dst;

                if (dstPiece != null)
                {
                    PiecesPositions[dstPiece.Color].Remove(dstPiece);
                }

                if (srcPiece is CastlingPiece castlingPiece)
                {
                    CastlingPiecesTouched[castlingPiece] = true;
                }

                this[move.Dst] = this[move.Src];
                this[move.Src] = null;
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            for (var row = 7; row > -1; row--)
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
