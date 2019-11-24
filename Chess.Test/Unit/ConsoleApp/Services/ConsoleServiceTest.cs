using Chess.ConsoleApp.Enums;
using Chess.ConsoleApp.Services;
using Chess.Domain.Enums;
using Chess.Domain.Factories;
using Chess.Domain.Models;
using FluentAssertions;
using Moq;
using Xunit;

namespace Chess.Test.Unit.ConsoleApp.Services
{
    public class ConsoleServiceTest
    {
        [Fact]
        public void DisplayTitle_ShouldDisplay()
        {
            // arrange
            var consoleWrapperMock = new Mock<IConsoleWrapper>();
            var sut = new ConsoleService(consoleWrapperMock.Object);

            // act
            sut.DisplayTitle();

            // assert
            consoleWrapperMock.Verify(mock => mock.WriteLine(@"
   ____ _                     _   _      _   
  / ___| |__   ___  ___ ___  | \ | | ___| |_ 
 | |   | '_ \ / _ \/ __/ __| |  \| |/ _ \ __|
 | |___| | | |  __/\__ \__ \_| |\  |  __/ |_ 
  \____|_| |_|\___||___/___(_)_| \_|\___|\__|
"));
        }

        [Theory]
        [InlineData('n', MainMenuSelection.NewGame)]
        [InlineData('l', MainMenuSelection.LoadGame)]
        public void RequestMainMenuSelection_ShouldHandleValidInput(char key, MainMenuSelection result)
        {
            // arrange
            var consoleWrapperMock = new Mock<IConsoleWrapper>();
            consoleWrapperMock
                .Setup(mock => mock.ReadKey())
                .Returns(key);

            var sut = new ConsoleService(consoleWrapperMock.Object);

            // act
            var selection = sut.RequestMainMenuSelection();

            // assert
            consoleWrapperMock.Verify(mock =>
                mock.Write("[n]ew game, [l]oad game: "));

            consoleWrapperMock.Verify(mock =>
                mock.ReadKey());

            consoleWrapperMock.Verify(mock =>
                mock.WriteLine(""));

            selection.Should().Be(result);
        }

        [Fact]
        public void RequestMainMenuSelection_ShouldHandleWrongInput()
        {
            // arrange
            var consoleWrapperMock = new Mock<IConsoleWrapper>();
            consoleWrapperMock
                .SetupSequence(mock => mock.ReadKey())
                .Returns('x')
                .Returns('n');

            var sut = new ConsoleService(consoleWrapperMock.Object);

            // act
            var selection = sut.RequestMainMenuSelection();

            // assert
            consoleWrapperMock.Verify(mock =>
                mock.Write("[n]ew game, [l]oad game: "), Times.Exactly(2));

            consoleWrapperMock.Verify(mock =>
                mock.ReadKey(), Times.Exactly(2));

            consoleWrapperMock.Verify(mock =>
                mock.WriteLine(""), Times.Exactly(2));

            selection.Should().Be(MainMenuSelection.NewGame);
        }

        [Theory]
        [InlineData('b', '3', PiecesColor.Black)]
        [InlineData('w', '3', PiecesColor.White)]
        public void RequestNewGameConfig_ShouldHandleValidInput(char key1, char key2, PiecesColor userColor)
        {
            // arrange
            const int recursionLevel = 3;

            var consoleWrapperMock = new Mock<IConsoleWrapper>();
            consoleWrapperMock
                .SetupSequence(mock => mock.ReadKey())
                .Returns(key1)
                .Returns(key2);

            var sut = new ConsoleService(consoleWrapperMock.Object);

            // act
            var (configUserColor, configRecursionLevel) = sut.RequestNewGameConfig();

            // assert
            consoleWrapperMock.Verify(mock =>
                mock.Write("[b]lack pieces, [w]hite pieces: "));

            consoleWrapperMock.Verify(mock =>
                mock.Write("recursion level (3 suggested): "));

            consoleWrapperMock.Verify(mock =>
                mock.ReadKey(), Times.Exactly(2));

            consoleWrapperMock.Verify(mock =>
                mock.WriteLine(""), Times.Exactly(2));

            configUserColor.Should().Be(userColor);
            configRecursionLevel.Should().Be(recursionLevel);
        }

        [Fact]
        public void RequestNewGameConfig_ShouldHandleWrongInput()
        {
            // arrange
            const int recursionLevel = 3;

            var consoleWrapperMock = new Mock<IConsoleWrapper>();
            consoleWrapperMock
                .SetupSequence(mock => mock.ReadKey())
                .Returns('x')
                .Returns('b')
                .Returns('x')
                .Returns('0')
                .Returns('3');

            var sut = new ConsoleService(consoleWrapperMock.Object);

            // act
            var (configUserColor, configRecursionLevel) = sut.RequestNewGameConfig();

            // assert
            consoleWrapperMock.Verify(mock =>
                mock.Write("[b]lack pieces, [w]hite pieces: "), Times.Exactly(2));

            consoleWrapperMock.Verify(mock =>
                mock.Write("recursion level (3 suggested): "), Times.Exactly(3));

            consoleWrapperMock.Verify(mock =>
                mock.ReadKey(), Times.Exactly(5));

            consoleWrapperMock.Verify(mock =>
                mock.WriteLine(""), Times.Exactly(5));

            configUserColor.Should().Be(PiecesColor.Black);
            configRecursionLevel.Should().Be(recursionLevel);
        }

        [Fact]
        public void DisplayBoard_GivenBlackFrontColor_ShouldDisplay()
        {
            // arrange
            var consoleWrapperMock = new Mock<IConsoleWrapper>();
            var sut = new ConsoleService(consoleWrapperMock.Object);

            var pieceFactoryMock = new Mock<IPieceFactory>();
            var board = new Board(pieceFactoryMock.Object);

            // act
            sut.DisplayBoard(board, PiecesColor.Black);

            // assert
            consoleWrapperMock.Verify(mock => mock.WriteLine(@"
1  R  N  B  K  Q  B  N  R 
2  P  P  P  P  P  P  P  P 
3                         
4                         
5                         
6                         
7  p  p  p  p  p  p  p  p 
8  r  n  b  k  q  b  n  r 
   h  g  f  e  d  c  b  a
"));
        }

        [Fact]
        public void DisplayBoard_GivenWhiteFrontColor_ShouldDisplay()
        {
            // arrange
            var consoleWrapperMock = new Mock<IConsoleWrapper>();
            var sut = new ConsoleService(consoleWrapperMock.Object);

            var pieceFactoryMock = new Mock<IPieceFactory>();
            var board = new Board(pieceFactoryMock.Object);

            // act
            sut.DisplayBoard(board, PiecesColor.White);

            // assert
            consoleWrapperMock.Verify(mock => mock.WriteLine(@"
8  r  n  b  q  k  b  n  r 
7  p  p  p  p  p  p  p  p 
6                         
5                         
4                         
3                         
2  P  P  P  P  P  P  P  P 
1  R  N  B  Q  K  B  N  R 
   a  b  c  d  e  f  g  h
"));
        }
    }
}
