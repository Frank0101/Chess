using Chess.Domain.Enums;
using Chess.Domain.Factories;
using Chess.Domain.Models;
using Chess.Domain.Models.Games;
using Chess.Domain.Models.Players;
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
            const PiecesColor userColor = PiecesColor.Black;
            const int recursionLevel = 3;

            var boardMock = new Mock<IBoard>();

            var boardFactoryMock = new Mock<IBoardFactory>();
            boardFactoryMock.Setup(mock => mock.Create())
                .Returns(boardMock.Object);

            var userPlayerMock = new Mock<IUserPlayer>();
            userPlayerMock.Setup(mock => mock.Color).Returns(userColor);

            var cpuPlayerMock = new Mock<ICpuPlayer>();
            cpuPlayerMock.Setup(mock => mock.Color).Returns(userColor.Invert());

            var playerFactoryMock = new Mock<IPlayerFactory>();
            playerFactoryMock
                .Setup(mock => mock.CreateUserPlayer(userColor))
                .Returns(userPlayerMock.Object);
            playerFactoryMock
                .Setup(mock => mock.CreateCpuPlayer(userColor.Invert(), recursionLevel))
                .Returns(cpuPlayerMock.Object);

            // act
            var game = new UserVsCpuGame(boardFactoryMock.Object, playerFactoryMock.Object,
                userColor, recursionLevel);

            // assert
            game.Board.Should().Be(boardMock.Object);
            game.UserPlayer.Should().Be(userPlayerMock.Object);
            game.CpuPlayer.Should().Be(cpuPlayerMock.Object);
        }
    }
}
