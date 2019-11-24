using AutoFixture;
using Chess.Domain.Enums;
using Chess.Domain.Models;
using Chess.Domain.Models.Pieces;
using FluentAssertions;
using Xunit;

namespace Chess.Test.Unit.Domain.Models.Pieces
{
    public class BishopTest
    {
        private readonly Fixture _fixture = new Fixture();

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
        [InlineData(PiecesColor.Black, true)]
        [InlineData(PiecesColor.Black, false)]
        [InlineData(PiecesColor.White, true)]
        [InlineData(PiecesColor.White, false)]
        public void IsMoveValid_ShouldReturnTrue(PiecesColor color, bool eating)
        {
            // arrange
            var bishop = new Bishop(color);
            var moveDescriptor = _fixture.Create<MoveDescriptor>();

            // act
            var result = bishop.IsMoveValid(moveDescriptor, eating);

            // assert
            result.Should().BeTrue();
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
