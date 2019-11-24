using Chess.Domain.Enums;
using Chess.Domain.Models;
using Chess.Domain.Models.Pieces;
using FluentAssertions;
using Xunit;

namespace Chess.Test.Unit.Domain.Models.Pieces
{
    public class PawnTest
    {
        private const int BlackRow = 1;
        private const int WhiteRow = 6;
        private const int Col = 3;

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
        [InlineData(PiecesColor.Black, BlackRow, Col, BlackRow + 1, Col, false)]
        [InlineData(PiecesColor.Black, BlackRow, Col, BlackRow + 2, Col, false)]
        [InlineData(PiecesColor.Black, BlackRow + 1, Col, BlackRow + 2, Col, false)]
        [InlineData(PiecesColor.Black, BlackRow, Col, BlackRow + 1, Col - 1, true)]
        [InlineData(PiecesColor.Black, BlackRow, Col, BlackRow + 1, Col + 1, true)]
        [InlineData(PiecesColor.White, WhiteRow, Col, WhiteRow - 1, Col, false)]
        [InlineData(PiecesColor.White, WhiteRow, Col, WhiteRow - 2, Col, false)]
        [InlineData(PiecesColor.White, WhiteRow - 1, Col, WhiteRow - 2, Col, false)]
        [InlineData(PiecesColor.White, WhiteRow, Col, WhiteRow - 1, Col - 1, true)]
        [InlineData(PiecesColor.White, WhiteRow, Col, WhiteRow - 1, Col + 1, true)]
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
        [InlineData(PiecesColor.Black, BlackRow, Col, BlackRow - 1, Col, false)]
        [InlineData(PiecesColor.Black, BlackRow, Col, BlackRow + 3, Col, false)]
        [InlineData(PiecesColor.Black, BlackRow + 1, Col, BlackRow + 3, Col, false)]
        [InlineData(PiecesColor.Black, BlackRow, Col, BlackRow + 1, Col, true)]
        [InlineData(PiecesColor.Black, BlackRow, Col, BlackRow + 1, Col - 1, false)]
        [InlineData(PiecesColor.Black, BlackRow, Col, BlackRow + 1, Col + 1, false)]
        [InlineData(PiecesColor.White, BlackRow, Col, BlackRow + 1, Col, false)]
        [InlineData(PiecesColor.White, BlackRow, Col, BlackRow - 3, Col, false)]
        [InlineData(PiecesColor.White, BlackRow - 1, Col, BlackRow - 3, Col, false)]
        [InlineData(PiecesColor.White, BlackRow, Col, BlackRow - 1, Col, true)]
        [InlineData(PiecesColor.White, BlackRow, Col, BlackRow - 1, Col - 1, false)]
        [InlineData(PiecesColor.White, BlackRow, Col, BlackRow - 1, Col + 1, false)]
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
