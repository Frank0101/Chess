using Chess.Domain.Enums;
using Chess.Domain.Models;
using Chess.Domain.Models.Pieces;
using FluentAssertions;
using Xunit;

namespace Chess.Test.Unit.Domain.Models.Pieces
{
    public class BishopTest
    {
        private const int Row = 3;
        private const int Col = 3;

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
        [InlineData(Row, Col, Row - 2, Col + 2)]
        [InlineData(Row, Col, Row + 2, Col + 2)]
        [InlineData(Row, Col, Row + 2, Col - 2)]
        [InlineData(Row, Col, Row - 2, Col - 2)]
        public void IsMoveValid_GivenValidMove_ShouldReturnTrue(
            int srcRow, int srcCol, int dstRow, int dstCol)
        {
            // arrange
            var bishop = new Bishop(PiecesColor.Black);
            var moveDescriptor = new MoveDescriptor(srcRow, srcCol, dstRow, dstCol);

            // act
            var result = bishop.IsMoveValid(moveDescriptor, false);

            // assert
            result.Should().BeTrue();
        }

        [Theory]
        [InlineData(Row, Col, Row - 2, Col)]
        [InlineData(Row, Col, Row, Col + 2)]
        [InlineData(Row, Col, Row + 2, Col)]
        [InlineData(Row, Col, Row, Col - 2)]
        public void IsValidMove_GivenInvalidMove_ShouldReturnFalse(
            int srcRow, int srcCol, int dstRow, int dstCol)
        {
            // arrange
            var bishop = new Bishop(PiecesColor.Black);
            var moveDescriptor = new MoveDescriptor(srcRow, srcCol, dstRow, dstCol);

            // act
            var result = bishop.IsMoveValid(moveDescriptor, false);

            // assert
            result.Should().BeFalse();
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
