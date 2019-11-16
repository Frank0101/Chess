using Chess.Domain.Enums;
using Chess.Domain.Models;
using Chess.Domain.Services;
using FluentAssertions;
using Xunit;

namespace Chess.Test.Unit.Domain.Services
{
    public class MoveValidationServiceTest
    {
        [Fact]
        public void ValidateMove_GivenBoardAndMoveDescriptor_ShouldValidate()
        {
            // arrange
            var sut = new MoveValidationService();

            var board = new Board(sut);
            var (srcRow, srcCol, dstRow, dstCol) = (1, 2, 3, 4);
            var moveDescriptor = new MoveDescriptor(srcRow, srcCol, dstRow, dstCol);

            // act
            var result = sut.ValidateMove(board, moveDescriptor);

            // assert
            result.Should().Be(MoveValidationResult.Valid);
        }
    }
}
