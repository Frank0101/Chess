using Chess.Domain.Enums;
using Chess.Domain.Models;
using Chess.Domain.Models.Pieces;
using FluentAssertions;
using Xunit;

namespace Chess.Test.Unit.Domain.Models.Pieces
{
    public class QueenTest
    {
        private const int Row = 3;
        private const int Col = 3;

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
        [InlineData(Row, Col, Row - 2, Col)]
        [InlineData(Row, Col, Row - 2, Col + 2)]
        [InlineData(Row, Col, Row, Col + 2)]
        [InlineData(Row, Col, Row + 2, Col + 2)]
        [InlineData(Row, Col, Row + 2, Col)]
        [InlineData(Row, Col, Row + 2, Col - 2)]
        [InlineData(Row, Col, Row, Col - 2)]
        [InlineData(Row, Col, Row - 2, Col - 2)]
        public void IsMoveValid_GivenValidMove_ShouldReturnTrue(
            int srcRow, int srcCol, int dstRow, int dstCol)
        {
            // arrange
            var queen = new Queen(PiecesColor.Black);
            var moveDescriptor = new MoveDescriptor(srcRow, srcCol, dstRow, dstCol);

            // act
            var result = queen.IsMoveValid(moveDescriptor, false);

            // assert
            result.Should().BeTrue();
        }

        [Theory]
        [InlineData(Row, Col, Row - 2, Col - 1)]
        [InlineData(Row, Col, Row - 2, Col + 1)]
        [InlineData(Row, Col, Row - 1, Col + 2)]
        [InlineData(Row, Col, Row + 1, Col + 2)]
        [InlineData(Row, Col, Row + 2, Col - 1)]
        [InlineData(Row, Col, Row + 2, Col + 1)]
        [InlineData(Row, Col, Row - 1, Col - 2)]
        [InlineData(Row, Col, Row + 1, Col - 2)]
        public void IsValidMove_GivenInvalidMove_ShouldReturnFalse(
            int srcRow, int srcCol, int dstRow, int dstCol)
        {
            // arrange
            var queen = new Queen(PiecesColor.Black);
            var moveDescriptor = new MoveDescriptor(srcRow, srcCol, dstRow, dstCol);

            // act
            var result = queen.IsMoveValid(moveDescriptor, false);

            // assert
            result.Should().BeFalse();
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
