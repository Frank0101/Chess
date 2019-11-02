using Chess.Domain.Enums;
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
