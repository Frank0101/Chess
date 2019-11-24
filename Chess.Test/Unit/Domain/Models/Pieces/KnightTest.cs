using Chess.Domain.Enums;
using Chess.Domain.Models;
using Chess.Domain.Models.Pieces;
using FluentAssertions;
using Xunit;

namespace Chess.Test.Unit.Domain.Models.Pieces
{
    public class KnightTest
    {
        [Theory]
        [InlineData(PiecesColor.Black, 'n')]
        [InlineData(PiecesColor.White, 'N')]
        public void Constructor_GivenColor_ShouldCreate(PiecesColor color, char symbol)
        {
            // act
            var knight = new Knight(color);

            // assert
            knight.Color.Should().Be(color);
            knight.Symbol.Should().Be(symbol);
            knight.Value.Should().Be(4);
        }

        [Theory]
        [InlineData(3, 3, 1, 4)]
        [InlineData(3, 3, 1, 2)]
        [InlineData(3, 3, 2, 5)]
        [InlineData(3, 3, 4, 5)]
        [InlineData(3, 3, 5, 4)]
        [InlineData(3, 3, 5, 2)]
        [InlineData(3, 3, 2, 1)]
        [InlineData(3, 3, 4, 1)]
        public void IsMoveValid_GivenValidMove_ShouldReturnTrue(
            int srcRow, int srcCol, int dstRow, int dstCol)
        {
            // arrange
            var knight = new Knight(PiecesColor.Black);
            var moveDescriptor = new MoveDescriptor(srcRow, srcCol, dstRow, dstCol);

            // act
            var result = knight.IsMoveValid(moveDescriptor, false);

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
            var knight = new Knight(PiecesColor.Black);
            var moveDescriptor = new MoveDescriptor(srcRow, srcCol, dstRow, dstCol);

            // act
            var result = knight.IsMoveValid(moveDescriptor, false);

            // assert
            result.Should().BeFalse();
        }

        [Theory]
        [InlineData(PiecesColor.Black, "n")]
        [InlineData(PiecesColor.White, "N")]
        public void ToString_ShouldPrintSymbol(PiecesColor color, string symbol)
        {
            // arrange
            var knight = new Knight(color);

            // act
            var knightString = knight.ToString();

            // assert
            knightString.Should().Be(symbol);
        }
    }
}
