using Chess.Domain.Enums;
using Chess.Domain.Factories;
using Chess.Domain.Models;
using Chess.Domain.Models.Pieces;
using FluentAssertions;
using Moq;
using Xunit;

namespace Chess.Test.Unit.Domain.Factories
{
    public class MoveFactoryTest
    {
        [Fact]
        public void Create_GivenBoardAndMoveDescriptor_ShouldCreate()
        {
            // arrange
            var sut = new MoveFactory();

            var srcPiece = new Pawn(PiecesColor.Black);
            var dstPiece = new Pawn(PiecesColor.White);
            var srcTile = new Tile(srcPiece);
            var dstTile = new Tile(dstPiece);

            var (srcRow, srcCol, dstRow, dstCol) = (1, 2, 3, 4);
            var moveDescriptor = new MoveDescriptor(srcRow, srcCol, dstRow, dstCol);

            var boardMock = new Mock<IBoard>();
            boardMock.Setup(mock => mock[srcRow, srcCol]).Returns(srcTile);
            boardMock.Setup(mock => mock[dstRow, dstCol]).Returns(dstTile);

            // act
            var move = sut.Create(boardMock.Object, moveDescriptor);

            // assert
            move.Should().NotBeNull();
            move.Should().BeOfType<Move>();
            move.SrcTile.Should().Be(srcTile);
            move.DstTile.Should().Be(dstTile);
        }
    }
}
