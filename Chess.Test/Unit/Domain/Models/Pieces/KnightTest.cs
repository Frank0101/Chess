using Chess.Domain.Enums;
using Chess.Domain.Models;
using Chess.Domain.Models.Pieces;
using FluentAssertions;
using Xunit;

namespace Chess.Test.Unit.Domain.Models.Pieces
{
    public class KnightTest
    {
        private const int Row = 3;
        private const int Col = 3;

        [Theory]
        [InlineData(PiecesColor.Black, 'n')]
        [InlineData(PiecesColor.White, 'N')]
        public void Constructor_GivenColor_ShouldCreate(PiecesColor color, char symbol)
        {
            // act
            var knight = new Knight(color);

            // assert
            knight.Color.Should().Be(color);
            knight.Symbol.Should().Be(symbol);
            knight.Value.Should().Be(4);
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
        public void IsMoveValid_GivenValidMove_ShouldReturnTrue(
            int srcRow, int srcCol, int dstRow, int dstCol)
        {
            // arrange
            var knight = new Knight(PiecesColor.Black);
            var moveDescriptor = new MoveDescriptor(srcRow, srcCol, dstRow, dstCol);

            // act
            var result = knight.IsMoveValid(moveDescriptor, false);

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
            var knight = new Knight(PiecesColor.Black);
            var moveDescriptor = new MoveDescriptor(srcRow, srcCol, dstRow, dstCol);

            // act
            var result = knight.IsMoveValid(moveDescriptor, false);

            // assert
            result.Should().BeFalse();
        }

        [Theory]
        [InlineData(PiecesColor.Black, "n")]
        [InlineData(PiecesColor.White, "N")]
        public void ToString_ShouldPrintSymbol(PiecesColor color, string symbol)
        {
            // arrange
            var knight = new Knight(color);

            // act
            var knightString = knight.ToString();

            // assert
            knightString.Should().Be(symbol);
        }
    }
}
