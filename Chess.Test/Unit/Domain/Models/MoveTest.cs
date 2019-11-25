using System;
using Chess.Domain.Enums;
using Chess.Domain.Models;
using Chess.Domain.Models.Pieces;
using FluentAssertions;
using Moq;
using Xunit;

namespace Chess.Test.Unit.Domain.Models
{
    public class MoveTest
    {
        [Fact]
        public void Constructor_GivenBoardAndMoveDescriptor_ShouldCreate()
        {
            // arrange
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
            var move = new Move(boardMock.Object, moveDescriptor);

            // assert
            move.SrcTile.Should().Be(srcTile);
            move.DstTile.Should().Be(dstTile);
        }

        [Fact]
        public void Apply_GivenValidTurnIndex_ShouldApply()
        {
            // arrange
            var srcPiece = new Pawn(PiecesColor.Black);
            var dstPiece = new Pawn(PiecesColor.White);
            var srcTile = new Tile(srcPiece);
            var dstTile = new Tile(dstPiece);

            var (srcRow, srcCol, dstRow, dstCol) = (1, 2, 3, 4);
            var moveDescriptor = new MoveDescriptor(srcRow, srcCol, dstRow, dstCol);

            var boardMock = new Mock<IBoard>();
            boardMock.Setup(mock => mock[srcRow, srcCol]).Returns(srcTile);
            boardMock.Setup(mock => mock[dstRow, dstCol]).Returns(dstTile);

            var move = new Move(boardMock.Object, moveDescriptor);

            // act
            move.Apply();

            // assert
            srcTile.Piece.Should().BeNull();
            dstTile.Piece.Should().Be(srcPiece);
        }

        [Fact]
        public void Apply_GivenInvalidTurnIndex_ShouldThrowException()
        {
            // arrange
            var srcPiece = new Pawn(PiecesColor.Black);
            var dstPiece = new Pawn(PiecesColor.White);
            var srcTile = new Tile(srcPiece);
            var dstTile = new Tile(dstPiece);

            var (srcRow, srcCol, dstRow, dstCol) = (1, 2, 3, 4);
            var moveDescriptor = new MoveDescriptor(srcRow, srcCol, dstRow, dstCol);

            var boardMock = new Mock<IBoard>();
            boardMock.Setup(mock => mock[srcRow, srcCol]).Returns(srcTile);
            boardMock.Setup(mock => mock[dstRow, dstCol]).Returns(dstTile);

            var move = new Move(boardMock.Object, moveDescriptor);
            Action apply = () => { move.Apply(); };

            // act
            boardMock.Setup(mock => mock.TurnIndex).Returns(1);
            apply.Should().Throw<InvalidOperationException>();

            srcTile.Piece.Should().Be(srcPiece);
            dstTile.Piece.Should().Be(dstPiece);
        }

        [Fact]
        public void Undo_GivenValidTurnIndex_ShouldUndo()
        {
            // arrange
            var srcPiece = new Pawn(PiecesColor.Black);
            var dstPiece = new Pawn(PiecesColor.White);
            var srcTile = new Tile(srcPiece);
            var dstTile = new Tile(dstPiece);

            var (srcRow, srcCol, dstRow, dstCol) = (1, 2, 3, 4);
            var moveDescriptor = new MoveDescriptor(srcRow, srcCol, dstRow, dstCol);

            var boardMock = new Mock<IBoard>();
            boardMock.Setup(mock => mock[srcRow, srcCol]).Returns(srcTile);
            boardMock.Setup(mock => mock[dstRow, dstCol]).Returns(dstTile);

            var move = new Move(boardMock.Object, moveDescriptor);

            move.Apply();

            // act
            move.Undo();

            // assert
            srcTile.Piece.Should().Be(srcPiece);
            dstTile.Piece.Should().Be(dstPiece);
        }

        [Fact]
        public void Undo_GivenInvalidTurnIndex_ShouldThrowException()
        {
            // arrange
            var srcPiece = new Pawn(PiecesColor.Black);
            var dstPiece = new Pawn(PiecesColor.White);
            var srcTile = new Tile(srcPiece);
            var dstTile = new Tile(dstPiece);

            var (srcRow, srcCol, dstRow, dstCol) = (1, 2, 3, 4);
            var moveDescriptor = new MoveDescriptor(srcRow, srcCol, dstRow, dstCol);

            var boardMock = new Mock<IBoard>();
            boardMock.Setup(mock => mock[srcRow, srcCol]).Returns(srcTile);
            boardMock.Setup(mock => mock[dstRow, dstCol]).Returns(dstTile);

            var move = new Move(boardMock.Object, moveDescriptor);
            Action undo = () => { move.Undo(); };

            move.Apply();

            // act
            boardMock.Setup(mock => mock.TurnIndex).Returns(1);
            undo.Should().Throw<InvalidOperationException>();

            srcTile.Piece.Should().BeNull();
            dstTile.Piece.Should().Be(srcPiece);
        }
    }
}
