using Chess.Domain.Enums;
using Chess.Domain.Models.Pieces;
using FluentAssertions;
using Xunit;

namespace Chess.Test.Unit.Domain.Models.Pieces
{
    public class BishopTest
    {
        [Theory]
        [InlineData(PiecesColor.Black, 'b')]
        [InlineData(PiecesColor.White, 'B')]
        public void Constructor_GivenColor_ShouldCreate(PiecesColor color, char symbol)
        {
            // act
            var bishop = new Bishop(color);

            // assert
            bishop.Color.Should().Be(color);
            bishop.Symbol.Should().Be(symbol);
            bishop.Value.Should().Be(3);
        }

        [Theory]
        [InlineData(PiecesColor.Black, "b")]
        [InlineData(PiecesColor.White, "B")]
        public void ToString_ShouldPrintSymbol(PiecesColor color, string symbol)
        {
            // arrange
            var bishop = new Bishop(color);

            // act
            var bishopString = bishop.ToString();

            // assert
            bishopString.Should().Be(symbol);
        }
    }
}
