using Chess.Domain.Enums;
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
