using Chess.Domain.Services;
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
            var moveValidationServiceMock = new Mock<IMoveValidationService>();
            var sut = new BoardFactory(moveValidationServiceMock.Object);

            // act
            var board = sut.Create();

            // assert
            board.Should().NotBeNull();
        }
    }
}
