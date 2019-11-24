using AutoFixture;
using Chess.Domain.Enums;
using Chess.Domain.Models;
using Chess.Domain.Models.Pieces;
using FluentAssertions;
using Xunit;

namespace Chess.Test.Unit.Domain.Models.Pieces
{
    public class KnightTest
    {
        private readonly Fixture _fixture = new Fixture();

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
        [InlineData(PiecesColor.Black, true)]
        [InlineData(PiecesColor.Black, false)]
        [InlineData(PiecesColor.White, true)]
        [InlineData(PiecesColor.White, false)]
        public void IsMoveValid_ShouldReturnTrue(PiecesColor color, bool eating)
        {
            // arrange
            var knight = new Knight(color);
            var moveDescriptor = _fixture.Create<MoveDescriptor>();

            // act
            var result = knight.IsMoveValid(moveDescriptor, eating);

            // assert
            result.Should().BeTrue();
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
