using Chess.Domain.Enums;
using Chess.Domain.Models.Pieces;
using FluentAssertions;
using Xunit;

namespace Chess.Test.Unit.Domain.Models.Pieces
{
    public class PawnTest
    {
        [Theory]
        [InlineData(PiecesColor.Black, 'p')]
        [InlineData(PiecesColor.White, 'P')]
        public void Constructor_GivenColor_ShouldCreate(PiecesColor color, char symbol)
        {
            // act
            var pawn = new Pawn(color);

            // assert
            pawn.Color.Should().Be(color);
            pawn.Symbol.Should().Be(symbol);
            pawn.Value.Should().Be(1);
        }

        [Theory]
        [InlineData(PiecesColor.Black, "p")]
        [InlineData(PiecesColor.White, "P")]
        public void ToString_ShouldPrintSymbol(PiecesColor color, string symbol)
        {
            // arrange
            var pawn = new Pawn(color);

            // act
            var pawnString = pawn.ToString();

            // assert
            pawnString.Should().Be(symbol);
        }
    }
}
