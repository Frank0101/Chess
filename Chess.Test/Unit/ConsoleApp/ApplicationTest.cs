using Chess.ConsoleApp;
using Chess.ConsoleApp.Enums;
using Chess.ConsoleApp.Models;
using Chess.ConsoleApp.Services;
using Chess.Domain.Enums;
using Chess.Domain.Models;
using Chess.Domain.Models.Games;
using Chess.Domain.Services;
using Moq;
using Xunit;

namespace Chess.Test.Unit.ConsoleApp
{
    public class ApplicationTest
    {
        [Fact]
        public void Start_GivenNewGameSelection_ShouldStartNewGame()
        {
            // arrange
            const PiecesColor userColor = PiecesColor.Black;
            const int recursionLevel = 3;

            var moveValidationServiceMock = new Mock<IMoveValidationService>();
            var board = new Board(moveValidationServiceMock.Object);

            var boardFactoryMock = new Mock<IBoardFactory>();
            boardFactoryMock
                .Setup(mock => mock.Create())
                .Returns(board);

            var game = new UserVsCpuGame(boardFactoryMock.Object, userColor, recursionLevel);

            var gameFactoryMock = new Mock<IGameFactory>();
            gameFactoryMock
                .Setup(mock => mock.CreateUserVsCpuGame(userColor, recursionLevel))
                .Returns(game);

            var consoleServiceMock = new Mock<IConsoleService>();
            consoleServiceMock
                .Setup(mock => mock.RequestMainMenuSelection())
                .Returns(MainMenuSelection.NewGame);

            consoleServiceMock
                .Setup(mock => mock.RequestNewGameConfig())
                .Returns(new NewGameConfig(PiecesColor.Black, recursionLevel));

            var application = new Application(gameFactoryMock.Object, consoleServiceMock.Object);

            // act
            application.Start();

            // assert
            consoleServiceMock.Verify(mock => mock.DisplayTitle());
            consoleServiceMock.Verify(mock => mock.RequestMainMenuSelection());
            consoleServiceMock.Verify(mock => mock.RequestNewGameConfig());
            gameFactoryMock.Verify(mock => mock.CreateUserVsCpuGame(userColor, recursionLevel));
        }
    }
}
