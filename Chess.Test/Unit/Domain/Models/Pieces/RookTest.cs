using AutoFixture;
using Chess.Domain.Enums;
using Chess.Domain.Models;
using Chess.Domain.Models.Pieces;
using FluentAssertions;
using Xunit;

namespace Chess.Test.Unit.Domain.Models.Pieces
{
    public class RookTest
    {
        private readonly Fixture _fixture = new Fixture();

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
        [InlineData(PiecesColor.Black, true)]
        [InlineData(PiecesColor.Black, false)]
        [InlineData(PiecesColor.White, true)]
        [InlineData(PiecesColor.White, false)]
        public void IsMoveValid_ShouldReturnTrue(PiecesColor color, bool eating)
        {
            // arrange
            var rook = new Rook(color);
            var moveDescriptor = _fixture.Create<MoveDescriptor>();

            // act
            var result = rook.IsMoveValid(moveDescriptor, eating);

            // assert
            result.Should().BeTrue();
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
