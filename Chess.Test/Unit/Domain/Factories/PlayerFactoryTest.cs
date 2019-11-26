using Chess.Domain.Enums;
using Chess.Domain.Factories;
using Chess.Domain.Models.Players;
using FluentAssertions;
using Xunit;

namespace Chess.Test.Unit.Domain.Factories
{
    public class PlayerFactoryTest
    {
        [Fact]
        public void CreateUserPlayer_GivenColor_ShouldCreateUserPlayer()
        {
            // arrange
            var sut = new PlayerFactory();

            // act
            var userPlayer = sut.CreateUserPlayer(PiecesColor.Black);

            // assert
            userPlayer.Should().NotBeNull();
            userPlayer.Should().BeOfType<UserPlayer>();
            userPlayer.Color.Should().Be(PiecesColor.Black);
        }

        [Fact]
        public void CreateCpuPlayer_GivenColorAndRecursion_ShouldCreateCpuPlayer()
        {
            // arrange
            const int recursionLevel = 3;

            var sut = new PlayerFactory();

            // act
            var cpuPlayer = sut.CreateCpuPlayer(PiecesColor.Black, recursionLevel);

            // assert
            cpuPlayer.Should().NotBeNull();
            cpuPlayer.Should().BeOfType<CpuPlayer>();
            cpuPlayer.Color.Should().Be(PiecesColor.Black);
            ((CpuPlayer) cpuPlayer).RecursionLevel.Should().Be(recursionLevel);
        }
    }
}
