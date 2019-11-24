using Chess.Domain.Enums;
using Chess.Domain.Models;
using Chess.Domain.Models.Pieces;
using FluentAssertions;
using Xunit;

namespace Chess.Test.Unit.Domain.Models.Pieces
{
    public class RookTest
    {
        private const int Row = 3;
        private const int Col = 3;

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
        [InlineData(Row, Col, Row - 2, Col)]
        [InlineData(Row, Col, Row, Col + 2)]
        [InlineData(Row, Col, Row + 2, Col)]
        [InlineData(Row, Col, Row, Col - 2)]
        public void IsMoveValid_GivenValidMove_ShouldReturnTrue(
            int srcRow, int srcCol, int dstRow, int dstCol)
        {
            // arrange
            var rook = new Rook(PiecesColor.Black);
            var moveDescriptor = new MoveDescriptor(srcRow, srcCol, dstRow, dstCol);

            // act
            var result = rook.IsMoveValid(moveDescriptor, false);

            // assert
            result.Should().BeTrue();
        }

        [Theory]
        [InlineData(Row, Col, Row - 2, Col + 2)]
        [InlineData(Row, Col, Row + 2, Col + 2)]
        [InlineData(Row, Col, Row + 2, Col - 2)]
        [InlineData(Row, Col, Row - 2, Col - 2)]
        public void IsValidMove_GivenInvalidMove_ShouldReturnFalse(
            int srcRow, int srcCol, int dstRow, int dstCol)
        {
            // arrange
            var rook = new Rook(PiecesColor.Black);
            var moveDescriptor = new MoveDescriptor(srcRow, srcCol, dstRow, dstCol);

            // act
            var result = rook.IsMoveValid(moveDescriptor, false);

            // assert
            result.Should().BeFalse();
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
