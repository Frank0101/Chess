using Chess.Domain.Enums;
using Chess.Domain.Models.Players;
using FluentAssertions;
using Xunit;

namespace Chess.Test.Unit.Domain.Models.Players
{
    public class CpuPlayerTest
    {
        [Theory]
        [InlineData(PiecesColor.Black)]
        [InlineData(PiecesColor.White)]
        public void Constructor_GivenColorAndRecursion_ShouldCreate(PiecesColor color)
        {
            // arrange
            const int recursionLevel = 3;

            // act
            var cpuPlayer = new CpuPlayer(color, recursionLevel);

            // assert
            cpuPlayer.Color.Should().Be(color);
            cpuPlayer.RecursionLevel.Should().Be(recursionLevel);
        }
    }
}
