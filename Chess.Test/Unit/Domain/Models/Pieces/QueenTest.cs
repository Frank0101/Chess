using Chess.Domain.Enums;
using Chess.Domain.Models;
using Chess.Domain.Models.Pieces;
using FluentAssertions;
using Xunit;

namespace Chess.Test.Unit.Domain.Models.Pieces
{
    public class QueenTest
    {
        [Theory]
        [InlineData(PiecesColor.Black, 'q')]
        [InlineData(PiecesColor.White, 'Q')]
        public void Constructor_GivenColor_ShouldCreate(PiecesColor color, char symbol)
        {
            // act
            var queen = new Queen(color);

            // assert
            queen.Color.Should().Be(color);
            queen.Symbol.Should().Be(symbol);
            queen.Value.Should().Be(10);
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
            var queen = new Queen(PiecesColor.Black);
            var moveDescriptor = new MoveDescriptor(srcRow, srcCol, dstRow, dstCol);

            // act
            var result = queen.IsMoveValid(moveDescriptor, false);

            // assert
            result.Should().BeTrue();
        }

        [Theory]
        [InlineData(3, 3, 1, 2)]
        [InlineData(3, 3, 1, 4)]
        [InlineData(3, 3, 2, 5)]
        [InlineData(3, 3, 4, 5)]
        [InlineData(3, 3, 5, 4)]
        [InlineData(3, 3, 5, 2)]
        [InlineData(3, 3, 4, 1)]
        [InlineData(3, 3, 2, 1)]
        public void IsValidMove_GivenInvalidMove_ShouldReturnFalse(
            int srcRow, int srcCol, int dstRow, int dstCol)
        {
            // arrange
            var queen = new Queen(PiecesColor.Black);
            var moveDescriptor = new MoveDescriptor(srcRow, srcCol, dstRow, dstCol);

            // act
            var result = queen.IsMoveValid(moveDescriptor, false);

            // assert
            result.Should().BeFalse();
        }

        [Theory]
        [InlineData(PiecesColor.Black, "q")]
        [InlineData(PiecesColor.White, "Q")]
        public void ToString_ShouldPrintSymbol(PiecesColor color, string symbol)
        {
            // arrange
            var queen = new Queen(color);

            // act
            var queenString = queen.ToString();

            // assert
            queenString.Should().Be(symbol);
        }
    }
}
