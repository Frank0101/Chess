using System;
using System.IO;
using System.Text;
using Chess.ConsoleApp.Enums;
using Chess.ConsoleApp.Services;
using Chess.Domain.Enums;
using Chess.Domain.Models;
using Chess.Domain.Services;
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
            var stringWriter = SetConsoleOutput();
            var sut = new ConsoleService();

            // act
            sut.DisplayTitle();

            // assert
            stringWriter.ToString().Should().Be(@"
   ____ _                     _   _      _   
  / ___| |__   ___  ___ ___  | \ | | ___| |_ 
 | |   | '_ \ / _ \/ __/ __| |  \| |/ _ \ __|
 | |___| | | |  __/\__ \__ \_| |\  |  __/ |_ 
  \____|_| |_|\___||___/___(_)_| \_|\___|\__|

");
        }

        [Theory]
        [InlineData("n", MainMenuSelection.NewGame)]
        [InlineData("l", MainMenuSelection.LoadGame)]
        public void RequestMainMenuSelection_ShouldHandleValidInput(string input, MainMenuSelection result)
        {
            // arrange
            var stringWriter = SetConsoleOutput();
            var sut = new ConsoleService();

            // act
            SetConsoleInput(input);
            var selection = sut.RequestMainMenuSelection();

            // assert
            stringWriter.ToString().Should().Be(
                "[n]ew game, [l]oad game: \n");

            selection.Should().Be(result);
        }

        [Fact]
        public void RequestMainMenuSelection_ShouldHandleWrongInput()
        {
            // arrange
            var stringWriter = SetConsoleOutput();
            var sut = new ConsoleService();

            // act
            SetConsoleInput("xn");
            var result = sut.RequestMainMenuSelection();

            // assert
            stringWriter.ToString().Should().Be(
                "[n]ew game, [l]oad game: \n"
                + "[n]ew game, [l]oad game: \n");

            result.Should().Be(MainMenuSelection.NewGame);
        }

        [Theory]
        [InlineData("b3", PiecesColor.Black)]
        [InlineData("w3", PiecesColor.White)]
        public void RequestNewGameConfig_ShouldHandleValidInput(string input, PiecesColor userColor)
        {
            // arrange
            var stringWriter = SetConsoleOutput();
            var sut = new ConsoleService();

            const int recursionLevel = 3;

            // act
            SetConsoleInput(input);
            var (configUserColor, configRecursionLevel) = sut.RequestNewGameConfig();

            // assert
            stringWriter.ToString().Should().Be(
                "[b]lack pieces, [w]hite pieces: \n"
                + "recursion level (3 suggested): \n");

            configUserColor.Should().Be(userColor);
            configRecursionLevel.Should().Be(recursionLevel);
        }

        [Fact]
        public void RequestNewGameConfig_ShouldHandleWrongInput()
        {
            // arrange
            var stringWriter = SetConsoleOutput();
            var sut = new ConsoleService();

            const int recursionLevel = 3;

            // act
            SetConsoleInput("xbx03");
            var (configUserColor, configRecursionLevel) = sut.RequestNewGameConfig();

            // assert
            stringWriter.ToString().Should().Be(
                "[b]lack pieces, [w]hite pieces: \n"
                + "[b]lack pieces, [w]hite pieces: \n"
                + "recursion level (3 suggested): \n"
                + "recursion level (3 suggested): \n"
                + "recursion level (3 suggested): \n");

            configUserColor.Should().Be(PiecesColor.Black);
            configRecursionLevel.Should().Be(recursionLevel);
        }

        [Fact]
        public void DisplayBoard_GivenBlackFrontColor_ShouldDisplay()
        {
            // arrange
            var stringWriter = SetConsoleOutput();
            var sut = new ConsoleService();

            var moveValidationServiceMock = new Mock<IMoveValidationService>();
            var board = new Board(moveValidationServiceMock.Object);

            // act
            sut.DisplayBoard(board, PiecesColor.Black);

            // assert
            stringWriter.ToString().Should().Be(@"
1  R  N  B  K  Q  B  N  R 
2  P  P  P  P  P  P  P  P 
3                         
4                         
5                         
6                         
7  p  p  p  p  p  p  p  p 
8  r  n  b  k  q  b  n  r 
   h  g  f  e  d  c  b  a
");
        }

        [Fact]
        public void DisplayBoard_GivenWhiteFrontColor_ShouldDisplay()
        {
            // arrange
            var stringWriter = SetConsoleOutput();
            var sut = new ConsoleService();

            var moveValidationServiceMock = new Mock<IMoveValidationService>();
            var board = new Board(moveValidationServiceMock.Object);

            // act
            sut.DisplayBoard(board, PiecesColor.White);

            // assert
            stringWriter.ToString().Should().Be(@"
8  r  n  b  q  k  b  n  r 
7  p  p  p  p  p  p  p  p 
6                         
5                         
4                         
3                         
2  P  P  P  P  P  P  P  P 
1  R  N  B  Q  K  B  N  R 
   a  b  c  d  e  f  g  h
");
        }

        private static void SetConsoleInput(string input)
        {
            var strReader = new StreamReader(new MemoryStream(
                Encoding.UTF8.GetBytes(input)));

            Console.SetIn(strReader);
        }

        private static StringWriter SetConsoleOutput()
        {
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            return stringWriter;
        }
    }
}
