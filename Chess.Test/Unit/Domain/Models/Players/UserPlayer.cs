using Chess.Domain.Enums;
using Chess.Domain.Models.Players;
using FluentAssertions;
using Xunit;

namespace Chess.Test.Unit.Domain.Models.Players
{
    public class UserPlayerTest
    {
        [Theory]
        [InlineData(PiecesColor.Black)]
        [InlineData(PiecesColor.White)]
        public void Constructor_GivenColorAndRecursion_ShouldCreate(PiecesColor color)
        {
            // act
            var userPlayer = new UserPlayer(color);

            // assert
            userPlayer.Color.Should().Be(color);
        }
    }
}
