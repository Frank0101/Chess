using Chess.Domain.Enums;
using FluentAssertions;
using Xunit;

namespace Chess.Test.Unit.Domain.Enums
{
    public class PiecesColorTest
    {
        [Fact]
        public void InvertShould_GivenColor_InvertColor()
        {
            // arrange
            const PiecesColor blackColor = PiecesColor.Black;
            const PiecesColor whiteColor = PiecesColor.White;

            // assert
            blackColor.Invert().Should().Be(whiteColor);
            whiteColor.Invert().Should().Be(blackColor);
        }
    }
}
