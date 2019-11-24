using AutoFixture;
using Chess.Domain.Enums;
using Chess.Domain.Models;
using Chess.Domain.Models.Pieces;
using FluentAssertions;
using Xunit;

namespace Chess.Test.Unit.Domain.Models.Pieces
{
    public class KingTest
    {
        private readonly Fixture _fixture = new Fixture();

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
        [InlineData(PiecesColor.Black, true)]
        [InlineData(PiecesColor.Black, false)]
        [InlineData(PiecesColor.White, true)]
        [InlineData(PiecesColor.White, false)]
        public void IsMoveValid_ShouldReturnTrue(PiecesColor color, bool eating)
        {
            // arrange
            var king = new King(color);
            var moveDescriptor = _fixture.Create<MoveDescriptor>();

            // act
            var result = king.IsMoveValid(moveDescriptor, eating);

            // assert
            result.Should().BeTrue();
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
