using Chess.Domain.Enums;
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
