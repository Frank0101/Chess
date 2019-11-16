using Chess.Domain.Enums;
using Chess.Domain.Models;
using Chess.Domain.Models.Games;
using Chess.Domain.Services;
using FluentAssertions;
using Moq;
using Xunit;

namespace Chess.Test.Unit.Domain.Models.Games
{
    public class UserVsCpuGameTest
    {
        [Fact]
        public void Constructor_GivenParameters_ShouldCreate()
        {
            // arrange
            var boardFactoryMock = new Mock<IBoardFactory>();
            const PiecesColor userColor = PiecesColor.Black;
            const int recursionLevel = 3;

            var moveValidationServiceMock = new Mock<IMoveValidationService>();
            var expectedBoard = new Board(moveValidationServiceMock.Object);

            boardFactoryMock
                .Setup(mock => mock.Create())
                .Returns(expectedBoard);

            // act
            var game = new UserVsCpuGame(boardFactoryMock.Object, userColor, recursionLevel);

            // assert
            boardFactoryMock.Verify(mock => mock.Create());

            game.UserPlayer.Color.Should().Be(userColor);

            game.CpuPlayer.Color.Should().Be(userColor.Invert());
            game.CpuPlayer.RecursionLevel.Should().Be(recursionLevel);

            game.Board.Should().Be(expectedBoard);
        }
    }
}
