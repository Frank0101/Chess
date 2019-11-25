using System.Diagnostics.CodeAnalysis;
using Chess.Domain.Factories;
using Chess.Domain.Models;
using Moq;
using Xunit;

namespace Chess.Test.Unit.Domain.Models
{
    public class BoardTest
    {
        [Fact]
        public void Constructor_GivenDependencies_ShouldCreate()
        {
//            // arrange
//            var pieceFactoryMock = new Mock<IPieceFactory>();
//
//            // act
//            var board = new Board(pieceFactoryMock.Object);
//
//            // arrange
//            board.TurnColor.Should().Be(PiecesColor.White);
//
//            board[0, 0].Piece.Should().BeEquivalentTo(new Rook(PiecesColor.Black));
//            board[0, 1].Piece.Should().BeEquivalentTo(new Knight(PiecesColor.Black));
//            board[0, 2].Piece.Should().BeEquivalentTo(new Bishop(PiecesColor.Black));
//            board[0, 3].Piece.Should().BeEquivalentTo(new Queen(PiecesColor.Black));
//            board[0, 4].Piece.Should().BeEquivalentTo(new King(PiecesColor.Black));
//            board[0, 5].Piece.Should().BeEquivalentTo(new Bishop(PiecesColor.Black));
//            board[0, 6].Piece.Should().BeEquivalentTo(new Knight(PiecesColor.Black));
//            board[0, 7].Piece.Should().BeEquivalentTo(new Rook(PiecesColor.Black));
//
//            for (var col = 0; col < 8; col++)
//            {
//                board[1, col].Piece.Should().BeEquivalentTo(new Pawn(PiecesColor.Black));
//            }
//
//            for (var row = 2; row < 6; row++)
//            {
//                for (var col = 0; col < 8; col++)
//                {
//                    board[row, col].Piece.Should().BeNull();
//                }
//            }
//
//            for (var col = 0; col < 8; col++)
//            {
//                board[6, col].Piece.Should().BeEquivalentTo(new Pawn(PiecesColor.White));
//            }
//
//            board[7, 0].Piece.Should().BeEquivalentTo(new Rook(PiecesColor.White));
//            board[7, 1].Piece.Should().BeEquivalentTo(new Knight(PiecesColor.White));
//            board[7, 2].Piece.Should().BeEquivalentTo(new Bishop(PiecesColor.White));
//            board[7, 3].Piece.Should().BeEquivalentTo(new Queen(PiecesColor.White));
//            board[7, 4].Piece.Should().BeEquivalentTo(new King(PiecesColor.White));
//            board[7, 5].Piece.Should().BeEquivalentTo(new Bishop(PiecesColor.White));
//            board[7, 6].Piece.Should().BeEquivalentTo(new Knight(PiecesColor.White));
//            board[7, 7].Piece.Should().BeEquivalentTo(new Rook(PiecesColor.White));
        }

        [Fact]
        public void TryCreateMove_GivenValidMoveDescriptor_ShouldSucceed()
        {
            // arrange
            var (srcRow, srcCol, dstRow, dstCol) = (1, 2, 3, 4);
            var moveDescriptor = new MoveDescriptor(srcRow, srcCol, dstRow, dstCol);

            var pieceFactoryMock = new Mock<IPieceFactory>();
//            pieceFactoryMock
//                .Setup(mock =>
//                    mock.ValidateMove(It.IsAny<Board>(), moveDescriptor))
//                .Returns(MoveValidationResult.Valid);

//            var board = new Board(pieceFactoryMock.Object);

            // act
//            var result = board.TryCreateMove(moveDescriptor, out var move);

            // assert
//            pieceFactoryMock.Verify(mock =>
//                mock.ValidateMove(board, moveDescriptor));

//            result.Should().Be(true);
//            move.Should().BeEquivalentTo(new Move(board, moveDescriptor));
        }

        [Fact]
        public void TryCreateMove_GivenInvalidMoveDescriptor_ShouldFail()
        {
            // arrange
            var (srcRow, srcCol, dstRow, dstCol) = (1, 2, 3, 4);
            var moveDescriptor = new MoveDescriptor(srcRow, srcCol, dstRow, dstCol);

            var pieceFactoryMock = new Mock<IPieceFactory>();
//            pieceFactoryMock
//                .Setup(mock =>
//                    mock.ValidateMove(It.IsAny<Board>(), moveDescriptor))
//                .Returns(MoveValidationResult.InvalidSrc);

//            var board = new Board(pieceFactoryMock.Object);

            // act
//            var result = board.TryCreateMove(moveDescriptor, out var move);

            // assert
//            pieceFactoryMock.Verify(mock =>
//                mock.ValidateMove(board, moveDescriptor));

//            result.Should().Be(false);
//            move.Should().BeNull();
        }

        [Fact]
        [SuppressMessage("ReSharper", "StringLiteralTypo")]
        public void ToString_ShouldPrintTiles()
        {
//            // arrange
//            var pieceFactoryMock = new Mock<IPieceFactory>();
//            var board = new Board(pieceFactoryMock.Object);
//
//            // act
//            var boardString = board.ToString();
//
//            // assert
//            boardString.Should().Be(
//                "rnbqkbnr\n"
//                + "pppppppp\n"
//                + "        \n"
//                + "        \n"
//                + "        \n"
//                + "        \n"
//                + "PPPPPPPP\n"
//                + "RNBQKBNR");
        }
    }
}
