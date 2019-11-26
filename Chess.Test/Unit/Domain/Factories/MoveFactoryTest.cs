using Chess.Domain.Factories;
using Chess.Domain.Models;
using FluentAssertions;
using Moq;
using Xunit;

namespace Chess.Test.Unit.Domain.Factories
{
    public class MoveFactoryTest
    {
        [Fact]
        public void Create_GivenBoardAndMoveDescriptor_ShouldCreate()
        {
            // arrange
            var boardMock = new Mock<IBoard>();

            var (srcRow, srcCol, dstRow, dstCol) = (1, 2, 3, 4);
            var moveDescriptor = new MoveDescriptor(srcRow, srcCol, dstRow, dstCol);

            var sut = new MoveFactory();

            // act
            var move = sut.Create(boardMock.Object, moveDescriptor);

            // assert
            move.Should().BeOfType<Move>();
        }
    }
}
