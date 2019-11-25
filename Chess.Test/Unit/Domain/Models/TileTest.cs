using Chess.Domain.Models;
using Chess.Domain.Models.Pieces;
using FluentAssertions;
using Moq;
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
            var pieceMock = new Mock<IPiece>();

            // act
            var tile = new Tile(pieceMock.Object);

            // assert
            tile.Piece.Should().Be(pieceMock.Object);
        }

        [Fact]
        public void PieceProperty_GivenPiece_ShouldSetPiece()
        {
            // arrange
            var tile = new Tile(null);
            var pieceMock = new Mock<IPiece>();

            // act
            tile.Piece = pieceMock.Object;

            // assert
            tile.Piece.Should().Be(pieceMock.Object);
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
            var pieceMock = new Mock<IPiece>();
            pieceMock.Setup(mock =>
                mock.ToString()).Returns("P");

            var tile = new Tile(pieceMock.Object);

            // act
            var tileString = tile.ToString();

            // assert
            tileString.Should().Be("P");
        }
    }
}
