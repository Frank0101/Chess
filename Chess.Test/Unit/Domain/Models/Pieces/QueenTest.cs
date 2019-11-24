using AutoFixture;
using Chess.Domain.Enums;
using Chess.Domain.Models;
using Chess.Domain.Models.Pieces;
using FluentAssertions;
using Xunit;

namespace Chess.Test.Unit.Domain.Models.Pieces
{
    public class QueenTest
    {
        private readonly Fixture _fixture = new Fixture();

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
        [InlineData(PiecesColor.Black, true)]
        [InlineData(PiecesColor.Black, false)]
        [InlineData(PiecesColor.White, true)]
        [InlineData(PiecesColor.White, false)]
        public void IsMoveValid_ShouldReturnTrue(PiecesColor color, bool eating)
        {
            // arrange
            var queen = new Queen(color);
            var moveDescriptor = _fixture.Create<MoveDescriptor>();

            // act
            var result = queen.IsMoveValid(moveDescriptor, eating);

            // assert
            result.Should().BeTrue();
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
