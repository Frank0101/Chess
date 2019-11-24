using Chess.Domain.Enums;
using Chess.Domain.Models;
using Chess.Domain.Models.Pieces;
using FluentAssertions;
using Xunit;

namespace Chess.Test.Unit.Domain.Models.Pieces
{
    public class KingTest
    {
        [Theory]
        [InlineData(PiecesColor.Black, 'k')]
        [InlineData(PiecesColor.White, 'K')]
        public void Constructor_GivenColor_ShouldCreate(PiecesColor color, char symbol)
        {
            // act
            var king = new King(color);

            // assert
            king.Color.Should().Be(color);
            king.Symbol.Should().Be(symbol);
            king.Value.Should().Be(99);
        }

        [Theory]
        [InlineData(3, 3, 1, 3)]
        [InlineData(3, 3, 1, 5)]
        [InlineData(3, 3, 3, 5)]
        [InlineData(3, 3, 5, 5)]
        [InlineData(3, 3, 5, 3)]
        [InlineData(3, 3, 5, 1)]
        [InlineData(3, 3, 3, 1)]
        [InlineData(3, 3, 1, 1)]
        public void IsMoveValid_GivenValidMove_ShouldReturnTrue(
            int srcRow, int srcCol, int dstRow, int dstCol)
        {
            // arrange
            var king = new King(PiecesColor.Black);
            var moveDescriptor = new MoveDescriptor(srcRow, srcCol, dstRow, dstCol);

            // act
            var result = king.IsMoveValid(moveDescriptor, false);

            // assert
            result.Should().BeTrue();
        }

        [Theory]
        [InlineData(3, 3, 1, 3)]
        [InlineData(3, 3, 1, 5)]
        [InlineData(3, 3, 3, 5)]
        [InlineData(3, 3, 5, 5)]
        [InlineData(3, 3, 5, 3)]
        [InlineData(3, 3, 5, 1)]
        [InlineData(3, 3, 3, 1)]
        [InlineData(3, 3, 1, 1)]
        public void IsValidMove_GivenInvalidMove_ShouldReturnFalse(
            int srcRow, int srcCol, int dstRow, int dstCol)
        {
            // arrange
            var king = new King(PiecesColor.Black);
            var moveDescriptor = new MoveDescriptor(srcRow, srcCol, dstRow, dstCol);

            // act
            var result = king.IsMoveValid(moveDescriptor, false);

            // assert
            result.Should().BeFalse();
        }

        [Theory]
        [InlineData(PiecesColor.Black, "k")]
        [InlineData(PiecesColor.White, "K")]
        public void ToString_ShouldPrintSymbol(PiecesColor color, string symbol)
        {
            // arrange
            var king = new King(color);

            // act
            var kingString = king.ToString();

            // assert
            kingString.Should().Be(symbol);
        }
    }
}
