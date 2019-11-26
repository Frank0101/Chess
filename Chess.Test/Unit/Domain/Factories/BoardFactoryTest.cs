using Chess.Domain.Factories;
using Chess.Domain.Models;
using FluentAssertions;
using Moq;
using Xunit;

namespace Chess.Test.Unit.Domain.Factories
{
    public class BoardFactoryTest
    {
        [Fact]
        public void Create_ShouldCreateBoard()
        {
            // arrange
            var pieceFactoryMock = new Mock<IPieceFactory>();
            var moveFactoryMock = new Mock<IMoveFactory>();
            var sut = new BoardFactory(pieceFactoryMock.Object, moveFactoryMock.Object);

            // act
            var board = sut.Create();

            // assert
            board.Should().NotBeNull();
            board.Should().BeOfType<Board>();
        }
    }
}
