using Chess.Domain.Enums;
using Chess.Domain.Models;
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
        [InlineData(PiecesColor.Black, 1, 3, 2, 3, false)]
        [InlineData(PiecesColor.Black, 1, 3, 3, 3, false)]
        [InlineData(PiecesColor.Black, 3, 3, 4, 3, false)]
        [InlineData(PiecesColor.Black, 4, 3, 5, 2, true)]
        [InlineData(PiecesColor.Black, 4, 3, 5, 4, true)]
        [InlineData(PiecesColor.White, 6, 3, 5, 3, false)]
        [InlineData(PiecesColor.White, 6, 3, 4, 3, false)]
        [InlineData(PiecesColor.White, 4, 3, 3, 3, false)]
        [InlineData(PiecesColor.White, 3, 3, 2, 2, true)]
        [InlineData(PiecesColor.White, 3, 3, 2, 4, true)]
        public void IsMoveValid_GivenValidMove_ShouldReturnTrue(PiecesColor color,
            int srcRow, int srcCol, int dstRow, int dstCol, bool eating)
        {
            // arrange
            var pawn = new Pawn(color);
            var moveDescriptor = new MoveDescriptor(srcRow, srcCol, dstRow, dstCol);

            // act
            var result = pawn.IsMoveValid(moveDescriptor, eating);

            // assert
            result.Should().BeTrue();
        }

        [Theory]
        [InlineData(PiecesColor.Black, 1, 3, 4, 3, false)]
        [InlineData(PiecesColor.Black, 2, 3, 4, 3, false)]
        [InlineData(PiecesColor.Black, 2, 3, 1, 3, false)]
        [InlineData(PiecesColor.Black, 2, 3, 3, 2, false)]
        [InlineData(PiecesColor.Black, 2, 3, 3, 4, false)]
        [InlineData(PiecesColor.Black, 2, 3, 3, 3, true)]
        [InlineData(PiecesColor.White, 6, 3, 3, 3, false)]
        [InlineData(PiecesColor.White, 5, 3, 3, 3, false)]
        [InlineData(PiecesColor.White, 5, 3, 6, 3, false)]
        [InlineData(PiecesColor.White, 5, 3, 4, 2, false)]
        [InlineData(PiecesColor.White, 5, 3, 4, 4, false)]
        [InlineData(PiecesColor.White, 5, 3, 4, 3, true)]
        public void IsValidMove_GivenInvalidMove_ShouldReturnFalse(PiecesColor color,
            int srcRow, int srcCol, int dstRow, int dstCol, bool eating)
        {
            // arrange
            var pawn = new Pawn(color);
            var moveDescriptor = new MoveDescriptor(srcRow, srcCol, dstRow, dstCol);

            // act
            var result = pawn.IsMoveValid(moveDescriptor, eating);

            // assert
            result.Should().BeFalse();
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
