using Chess.ConsoleApp.Models;
using Chess.Domain.Enums;
using FluentAssertions;
using Xunit;

namespace Chess.Test.Unit.ConsoleApp.Models
{
    public class NewGameConfigTest
    {
        [Theory]
        [InlineData(PiecesColor.Black)]
        [InlineData(PiecesColor.White)]
        public void Constructor_GivenUserColorAndRecursion_ShouldCreate(PiecesColor userColor)
        {
            // arrange
            const int recursionLevel = 3;

            // act
            var (configUserColor, configRecursionLevel) = new NewGameConfig(userColor, recursionLevel);

            // Assert
            configUserColor.Should().Be(userColor);
            configRecursionLevel.Should().Be(recursionLevel);
        }
    }
}
