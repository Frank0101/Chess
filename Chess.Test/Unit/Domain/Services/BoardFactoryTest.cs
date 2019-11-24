using Chess.Domain.Factories;
using FluentAssertions;
using Moq;
using Xunit;

namespace Chess.Test.Unit.Domain.Services
{
    public class BoardFactoryTest
    {
        [Fact]
        public void Create_ShouldCreateBoard()
        {
            // arrange
            var pieceFactoryMock = new Mock<IPieceFactory>();
            var sut = new BoardFactory(pieceFactoryMock.Object);

            // act
            var board = sut.Create();

            // assert
            board.Should().NotBeNull();
        }
    }
}
