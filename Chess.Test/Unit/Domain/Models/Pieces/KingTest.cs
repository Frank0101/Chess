using Chess.Domain.Enums;
using Chess.Domain.Models;
using Chess.Domain.Models.Pieces;
using FluentAssertions;
using Xunit;

namespace Chess.Test.Unit.Domain.Models.Pieces
{
    public class KingTest
    {
        private const int Row = 3;
        private const int Col = 3;

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
        [InlineData(Row, Col, Row - 1, Col)]
        [InlineData(Row, Col, Row - 1, Col + 1)]
        [InlineData(Row, Col, Row, Col + 1)]
        [InlineData(Row, Col, Row + 1, Col + 1)]
        [InlineData(Row, Col, Row + 1, Col)]
        [InlineData(Row, Col, Row + 1, Col - 1)]
        [InlineData(Row, Col, Row, Col - 1)]
        [InlineData(Row, Col, Row - 1, Col - 1)]
        public void IsMoveValid_GivenValidMove_ShouldReturnTrue(
            int srcRow, int srcCol, int dstRow, int dstCol)
        {
            // arrange
            var king = new King(PiecesColor.Black);
            var moveDescriptor = new MoveDescriptor(srcRow, srcCol, dstRow, dstCol);

            // act
            var result = king.IsMoveValid(moveDescriptor, false);

            // assert
            result.Should().BeTrue();
        }

        [Theory]
        [InlineData(Row, Col, Row - 2, Col)]
        [InlineData(Row, Col, Row - 2, Col + 2)]
        [InlineData(Row, Col, Row, Col + 2)]
        [InlineData(Row, Col, Row + 2, Col + 2)]
        [InlineData(Row, Col, Row + 2, Col)]
        [InlineData(Row, Col, Row + 2, Col - 2)]
        [InlineData(Row, Col, Row, Col - 2)]
        [InlineData(Row, Col, Row - 2, Col - 2)]
        public void IsValidMove_GivenInvalidMove_ShouldReturnFalse(
            int srcRow, int srcCol, int dstRow, int dstCol)
        {
            // arrange
            var king = new King(PiecesColor.Black);
            var moveDescriptor = new MoveDescriptor(srcRow, srcCol, dstRow, dstCol);

            // act
            var result = king.IsMoveValid(moveDescriptor, false);

            // assert
            result.Should().BeFalse();
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
