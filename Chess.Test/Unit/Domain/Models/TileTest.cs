using Chess.Domain.Enums;
using Chess.Domain.Models;
using Chess.Domain.Models.Pieces;
using FluentAssertions;
using Xunit;

namespace Chess.Test.Unit.Domain.Models
{
    public class TileTest
    {
        [Fact]
        public void Constructor_GivenNull_ShouldCreateEmptyTile()
        {
            // act
            var tile = new Tile(null);

            // assert
            tile.Piece.Should().BeNull();
        }

        [Fact]
        public void Constructor_GivenPiece_ShouldCreateTileWithPiece()
        {
            // arrange
            var piece = new Pawn(PiecesColor.Black);

            // act
            var tile = new Tile(piece);

            // assert
            tile.Piece.Should().Be(piece);
        }

        [Fact]
        public void PieceProperty_GivenPiece_ShouldSetPiece()
        {
            // arrange
            var tile = new Tile(null);
            var piece = new Pawn(PiecesColor.Black);

            // act
            tile.Piece = piece;

            // assert
            tile.Piece.Should().Be(piece);
        }

        [Fact]
        public void ToString_GivenEmptyTile_ShouldPrintSpace()
        {
            // arrange
            var tile = new Tile(null);

            // act
            var tileString = tile.ToString();

            // assert
            tileString.Should().Be(" ");
        }

        [Fact]
        public void ToString_GivenTileWithPiece_ShouldPrintPiece()
        {
            // arrange
            var piece = new Pawn(PiecesColor.Black);
            var tile = new Tile(new Pawn(PiecesColor.Black));

            // act
            var tileString = tile.ToString();

            // assert
            tileString.Should().Be(piece.ToString());
        }
    }
}
