using Chess.Domain.Enums;
using Chess.Domain.Services;
using FluentAssertions;
using Moq;
using Xunit;

namespace Chess.Test.Unit.Domain.Services
{
    public class GameFactoryTest
    {
        [Fact]
        public void CreateUserVsCpuGame_ShouldCreateGame()
        {
            // arrange
            var boardFactoryMock = new Mock<IBoardFactory>();
            var sut = new GameFactory(boardFactoryMock.Object);

            const PiecesColor userColor = PiecesColor.Black;
            const int recursionLevel = 3;

            // act
            var game = sut.CreateUserVsCpuGame(userColor, recursionLevel);

            // assert
            game.UserPlayer.Color.Should().Be(userColor);
            game.CpuPlayer.RecursionLevel.Should().Be(recursionLevel);
        }
    }
}
