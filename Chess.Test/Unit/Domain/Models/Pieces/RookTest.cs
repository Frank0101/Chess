using Chess.Domain.Enums;
using Chess.Domain.Models;
using Chess.Domain.Models.Pieces;
using FluentAssertions;
using Xunit;

namespace Chess.Test.Unit.Domain.Models.Pieces
{
    public class RookTest
    {
        [Theory]
        [InlineData(PiecesColor.Black, 'r')]
        [InlineData(PiecesColor.White, 'R')]
        public void Constructor_GivenColor_ShouldCreate(PiecesColor color, char symbol)
        {
            // act
            var rook = new Rook(color);

            // assert
            rook.Color.Should().Be(color);
            rook.Symbol.Should().Be(symbol);
            rook.Value.Should().Be(5);
        }

        [Theory]
        [InlineData(3, 3, 1, 3)]
        [InlineData(3, 3, 3, 5)]
        [InlineData(3, 3, 5, 3)]
        [InlineData(3, 3, 3, 1)]
        public void IsMoveValid_GivenValidMove_ShouldReturnTrue(
            int srcRow, int srcCol, int dstRow, int dstCol)
        {
            // arrange
            var rook = new Rook(PiecesColor.Black);
            var moveDescriptor = new MoveDescriptor(srcRow, srcCol, dstRow, dstCol);

            // act
            var result = rook.IsMoveValid(moveDescriptor, false);

            // assert
            result.Should().BeTrue();
        }

        [Theory]
        [InlineData(3, 3, 1, 5)]
        [InlineData(3, 3, 5, 5)]
        [InlineData(3, 3, 1, 1)]
        [InlineData(3, 3, 5, 1)]
        public void IsValidMove_GivenInvalidMove_ShouldReturnFalse(
            int srcRow, int srcCol, int dstRow, int dstCol)
        {
            // arrange
            var rook = new Rook(PiecesColor.Black);
            var moveDescriptor = new MoveDescriptor(srcRow, srcCol, dstRow, dstCol);

            // act
            var result = rook.IsMoveValid(moveDescriptor, false);

            // assert
            result.Should().BeFalse();
        }

        [Theory]
        [InlineData(PiecesColor.Black, "r")]
        [InlineData(PiecesColor.White, "R")]
        public void ToString_ShouldPrintSymbol(PiecesColor color, string symbol)
        {
            // arrange
            var rook = new Rook(color);

            // act
            var rookString = rook.ToString();

            // assert
            rookString.Should().Be(symbol);
        }
    }
}
