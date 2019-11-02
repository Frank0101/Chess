using Chess.Domain.Models;
using FluentAssertions;
using Xunit;

namespace Chess.Test.Unit.Domain.Models
{
    public class MoveDescriptorTest
    {
        [Fact]
        public void Constructor_GivenSrcAndDst_ShouldCreate()
        {
            // arrange
            var (srcRow, srcCol, dstRow, dstCol) = (1, 2, 3, 4);

            // act
            var moveDescriptor = new MoveDescriptor(srcRow, srcCol, dstRow, dstCol);

            // assert
            moveDescriptor.SrcRow.Should().Be(srcRow);
            moveDescriptor.SrcCol.Should().Be(srcCol);
            moveDescriptor.DstRow.Should().Be(dstRow);
            moveDescriptor.DstCol.Should().Be(dstCol);
        }
    }
}
