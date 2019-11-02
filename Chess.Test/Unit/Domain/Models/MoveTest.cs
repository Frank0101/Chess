using Chess.Domain.Models;
using Chess.Domain.Services;
using FluentAssertions;
using Moq;
using Xunit;

namespace Chess.Test.Unit.Domain.Models
{
    public class MoveTest
    {
        [Fact]
        public void Constructor_GivenBoardAndMoveDescriptor_ShouldCreate()
        {
            // arrange
            var moveValidationServiceMock = new Mock<IMoveValidationService>();
            var board = new Board(moveValidationServiceMock.Object);

            var (srcRow, srcCol, dstRow, dstCol) = (1, 2, 3, 4);
            var moveDescriptor = new MoveDescriptor(srcRow, srcCol, dstRow, dstCol);

            // act
            var move = new Move(board, moveDescriptor);

            // assert
            move.SrcTile.Should().Be(board[srcRow, srcCol]);
            move.DstTile.Should().Be(board[dstRow, dstCol]);
        }
    }
}
