using System.Diagnostics.CodeAnalysis;
using Chess.Domain.Enums;
using Chess.Domain.Factories;
using Chess.Domain.Models;
using Chess.Domain.Models.Pieces;
using FluentAssertions;
using Moq;
using Xunit;

namespace Chess.Test.Unit.Domain.Models
{
    public class BoardTest
    {
        [Fact]
        public void Constructor_GivenDependencies_ShouldCreate()
        {
            // arrange
            var (
                blackPawnMock,
                blackBishopMock,
                blackKnightMock,
                blackRookMock,
                blackQueenMock,
                blackKingMock,
                whitePawnMock,
                whiteBishopMock,
                whiteKnightMock,
                whiteRookMock,
                whiteQueenMock,
                whiteKingMock,
                pieceFactoryMock) = MockPieces();

            var moveFactoryMock = new Mock<IMoveFactory>();

            // act
            var board = new Board(pieceFactoryMock.Object, moveFactoryMock.Object);

            // assert
            board.TurnColor.Should().Be(PiecesColor.White);
            board.TurnIndex.Should().Be(0);

            board[0, 0].Piece.Should().Be(blackRookMock.Object);
            board[0, 1].Piece.Should().Be(blackKnightMock.Object);
            board[0, 2].Piece.Should().Be(blackBishopMock.Object);
            board[0, 3].Piece.Should().Be(blackQueenMock.Object);
            board[0, 4].Piece.Should().Be(blackKingMock.Object);
            board[0, 5].Piece.Should().Be(blackBishopMock.Object);
            board[0, 6].Piece.Should().Be(blackKnightMock.Object);
            board[0, 7].Piece.Should().Be(blackRookMock.Object);

            for (var col = 0; col < 8; col++)
            {
                board[1, col].Piece.Should().Be(blackPawnMock.Object);
            }

            for (var row = 2; row < 6; row++)
            {
                for (var col = 0; col < 8; col++)
                {
                    board[row, col].Piece.Should().BeNull();
                }
            }

            for (var col = 0; col < 8; col++)
            {
                board[6, col].Piece.Should().Be(whitePawnMock.Object);
            }

            board[7, 0].Piece.Should().Be(whiteRookMock.Object);
            board[7, 1].Piece.Should().Be(whiteKnightMock.Object);
            board[7, 2].Piece.Should().Be(whiteBishopMock.Object);
            board[7, 3].Piece.Should().Be(whiteQueenMock.Object);
            board[7, 4].Piece.Should().Be(whiteKingMock.Object);
            board[7, 5].Piece.Should().Be(whiteBishopMock.Object);
            board[7, 6].Piece.Should().Be(whiteKnightMock.Object);
            board[7, 7].Piece.Should().Be(whiteRookMock.Object);
        }

        [Theory]
        [InlineData(5, 0, 4, 0)]
        [InlineData(1, 0, 2, 0)]
        public void TryCreateMove_GivenInvalidSrc_ShouldFail(
            int srcRow, int srcCol, int dstRow, int dstCol)
        {
            // arrange
            var (
                _, _, _, _, _, _, _, _, _, _, _, _,
                pieceFactoryMock) = MockPieces();

            var moveFactoryMock = new Mock<IMoveFactory>();
            var board = new Board(pieceFactoryMock.Object, moveFactoryMock.Object);

            var moveDescriptor = new MoveDescriptor(srcRow, srcCol, dstRow, dstCol);

            // act
            var result = board.TryCreateMove(moveDescriptor, out var move);

            // assert
            result.Should().Be(MoveValidationResult.InvalidSrc);
            move.Should().BeNull();
        }

        [Fact]
        public void TryCreateMove_GivenInvalidDst_ShouldFail()
        {
            // arrange
            var (
                _, _, _, _, _, _, _, _, _, _, _, _,
                pieceFactoryMock) = MockPieces();

            var moveFactoryMock = new Mock<IMoveFactory>();
            var board = new Board(pieceFactoryMock.Object, moveFactoryMock.Object);

            var (srcRow, srcCol, dstRow, dstCol) = (7, 0, 6, 0);
            var moveDescriptor = new MoveDescriptor(srcRow, srcCol, dstRow, dstCol);

            // act
            var result = board.TryCreateMove(moveDescriptor, out var move);

            // assert
            result.Should().Be(MoveValidationResult.InvalidDst);
            move.Should().BeNull();
        }

        [Fact]
        public void TryCreateMove_GivenInvalidMove_ShouldFail()
        {
            // arrange
            var (
                _, _, _, _, _, _, _, _, _,
                whiteRookMock,
                _, _,
                pieceFactoryMock) = MockPieces();

            var moveFactoryMock = new Mock<IMoveFactory>();
            var board = new Board(pieceFactoryMock.Object, moveFactoryMock.Object);

            var (srcRow, srcCol, dstRow, dstCol) = (7, 0, 1, 0);
            var moveDescriptor = new MoveDescriptor(srcRow, srcCol, dstRow, dstCol);

            whiteRookMock.Setup(mock => mock.IsMoveValid(moveDescriptor, true))
                .Returns(false);

            // act
            var result = board.TryCreateMove(moveDescriptor, out var move);

            // assert
            whiteRookMock.Verify(mock => mock.IsMoveValid(moveDescriptor, true));

            result.Should().Be(MoveValidationResult.InvalidMove);
            move.Should().BeNull();
        }

        [Fact]
        public void TryCreateMove_GivenInvalidPath_ShouldFail()
        {
            // arrange
            var (
                _, _, _, _, _, _, _, _, _,
                whiteRookMock,
                _, _,
                pieceFactoryMock) = MockPieces();

            var moveFactoryMock = new Mock<IMoveFactory>();
            var board = new Board(pieceFactoryMock.Object, moveFactoryMock.Object);

            var (srcRow, srcCol, dstRow, dstCol) = (7, 0, 1, 0);
            var moveDescriptor = new MoveDescriptor(srcRow, srcCol, dstRow, dstCol);

            whiteRookMock.Setup(mock => mock.IsMoveValid(moveDescriptor, true))
                .Returns(true);

            // act
            var result = board.TryCreateMove(moveDescriptor, out var move);

            // assert
            whiteRookMock.Verify(mock => mock.IsMoveValid(moveDescriptor, true));

            result.Should().Be(MoveValidationResult.InvalidPath);
            move.Should().BeNull();
        }

        [Fact]
        public void TryCreateMove_GivenValidMove_ShouldSucceed()
        {
            // arrange
            var (
                _, _, _, _, _, _,
                whitePawnMock,
                _, _, _, _, _,
                pieceFactoryMock) = MockPieces();

            var moveFactoryMock = new Mock<IMoveFactory>();
            var board = new Board(pieceFactoryMock.Object, moveFactoryMock.Object);

            var (srcRow, srcCol, dstRow, dstCol) = (6, 0, 4, 0);
            var moveDescriptor = new MoveDescriptor(srcRow, srcCol, dstRow, dstCol);

            whitePawnMock.Setup(mock => mock.IsMoveValid(moveDescriptor, false))
                .Returns(true);

            var moveMock = new Mock<IMove>();
            moveFactoryMock.Setup(mock => mock.Create(board, moveDescriptor))
                .Returns(moveMock.Object);

            // act
            var result = board.TryCreateMove(moveDescriptor, out var move);

            // assert
            whitePawnMock.Verify(mock => mock.IsMoveValid(moveDescriptor, false));
            moveFactoryMock.Verify(mock => mock.Create(board, moveDescriptor));

            result.Should().Be(MoveValidationResult.Valid);
            move.Should().Be(moveMock.Object);
        }

        [Fact]
        public void TryCreateMove_GivenKnightJumping_ShouldSucceed()
        {
            // arrange
            var (
                _, _, _, _, _, _, _, _,
                whiteKnightMock,
                _, _, _,
                pieceFactoryMock) = MockPieces();

            var moveFactoryMock = new Mock<IMoveFactory>();
            var board = new Board(pieceFactoryMock.Object, moveFactoryMock.Object);

            var (srcRow, srcCol, dstRow, dstCol) = (7, 1, 5, 0);
            var moveDescriptor = new MoveDescriptor(srcRow, srcCol, dstRow, dstCol);

            whiteKnightMock.Setup(mock => mock.IsMoveValid(moveDescriptor, false))
                .Returns(true);

            var moveMock = new Mock<IMove>();
            moveFactoryMock.Setup(mock => mock.Create(board, moveDescriptor))
                .Returns(moveMock.Object);

            // act
            var result = board.TryCreateMove(moveDescriptor, out var move);

            // assert
            whiteKnightMock.Verify(mock => mock.IsMoveValid(moveDescriptor, false));
            moveFactoryMock.Verify(mock => mock.Create(board, moveDescriptor));

            result.Should().Be(MoveValidationResult.Valid);
            move.Should().Be(moveMock.Object);
        }

        [Fact]
        public void ApplyMove_GivenMove_ShouldApply()
        {
            // arrange
            var (
                _, _, _, _, _, _, _, _, _, _, _, _,
                pieceFactoryMock) = MockPieces();

            var moveFactoryMock = new Mock<IMoveFactory>();
            var board = new Board(pieceFactoryMock.Object, moveFactoryMock.Object);

            var moveMock = new Mock<IMove>();

            // act
            board.ApplyMove(moveMock.Object);

            // assert
            moveMock.Verify(mock => mock.Apply());

            board.TurnColor.Should().Be(PiecesColor.Black);
            board.TurnIndex.Should().Be(1);
        }

        [Fact]
        public void UndoLastMove_ShouldUndoLastMove()
        {
            // arrange
            var (
                _, _, _, _, _, _, _, _, _, _, _, _,
                pieceFactoryMock) = MockPieces();

            var moveFactoryMock = new Mock<IMoveFactory>();
            var board = new Board(pieceFactoryMock.Object, moveFactoryMock.Object);

            var moveMock = new Mock<IMove>();
            board.ApplyMove(moveMock.Object);

            // act
            var lastMove = board.UndoLastMove();

            // assert
            moveMock.Verify(mock => mock.Undo());

            lastMove.Should().Be(moveMock.Object);
            board.TurnColor.Should().Be(PiecesColor.White);
            board.TurnIndex.Should().Be(0);
        }

        [Fact]
        [SuppressMessage("ReSharper", "StringLiteralTypo")]
        public void ToString_ShouldPrintTiles()
        {
            // arrange
            var (
                _, _, _, _, _, _, _, _, _, _, _, _,
                pieceFactoryMock) = MockPieces();

            var moveFactoryMock = new Mock<IMoveFactory>();
            var board = new Board(pieceFactoryMock.Object, moveFactoryMock.Object);

            // act
            var boardString = board.ToString();

            // assert
            const string expectedString = "rnbqkbnr\n"
                                          + "pppppppp\n"
                                          + "        \n"
                                          + "        \n"
                                          + "        \n"
                                          + "        \n"
                                          + "PPPPPPPP\n"
                                          + "RNBQKBNR";

            boardString.Should().Be(expectedString);
        }

        private static (
            Mock<IPiece> blackPawnMock,
            Mock<IPiece> blackBishopMock,
            Mock<IPiece> blackKnightMock,
            Mock<IPiece> blackRookMock,
            Mock<IPiece> blackQueenMock,
            Mock<IPiece> blackKingMock,
            Mock<IPiece> whitePawnMock,
            Mock<IPiece> whiteBishopMock,
            Mock<IPiece> whiteKnightMock,
            Mock<IPiece> whiteRookMock,
            Mock<IPiece> whiteQueenMock,
            Mock<IPiece> whiteKingMock,
            Mock<IPieceFactory> pieceFactoryMock)
            MockPieces()
        {
            var blackPawnMock = new Mock<IPiece>();
            blackPawnMock.Setup(mock => mock.Color).Returns(PiecesColor.Black);
            blackPawnMock.Setup(mock => mock.ToString()).Returns("p");

            var blackBishopMock = new Mock<IPiece>();
            blackBishopMock.Setup(mock => mock.Color).Returns(PiecesColor.Black);
            blackBishopMock.Setup(mock => mock.ToString()).Returns("b");

            var blackKnightMock = new Mock<IPiece>();
            blackKnightMock.Setup(mock => mock.Color).Returns(PiecesColor.Black);
            blackKnightMock.Setup(mock => mock.ToString()).Returns("n");

            var blackRookMock = new Mock<IPiece>();
            blackRookMock.Setup(mock => mock.Color).Returns(PiecesColor.Black);
            blackRookMock.Setup(mock => mock.ToString()).Returns("r");

            var blackQueenMock = new Mock<IPiece>();
            blackQueenMock.Setup(mock => mock.Color).Returns(PiecesColor.Black);
            blackQueenMock.Setup(mock => mock.ToString()).Returns("q");

            var blackKingMock = new Mock<IPiece>();
            blackKingMock.Setup(mock => mock.Color).Returns(PiecesColor.Black);
            blackKingMock.Setup(mock => mock.ToString()).Returns("k");

            var whitePawnMock = new Mock<IPiece>();
            whitePawnMock.Setup(mock => mock.Color).Returns(PiecesColor.White);
            whitePawnMock.Setup(mock => mock.ToString()).Returns("P");

            var whiteBishopMock = new Mock<IPiece>();
            whiteBishopMock.Setup(mock => mock.Color).Returns(PiecesColor.White);
            whiteBishopMock.Setup(mock => mock.ToString()).Returns("B");

            var whiteKnightMock = new Mock<IPiece>();
            whiteKnightMock.Setup(mock => mock.Color).Returns(PiecesColor.White);
            whiteKnightMock.Setup(mock => mock.ToString()).Returns("N");

            var whiteRookMock = new Mock<IPiece>();
            whiteRookMock.Setup(mock => mock.Color).Returns(PiecesColor.White);
            whiteRookMock.Setup(mock => mock.ToString()).Returns("R");

            var whiteQueenMock = new Mock<IPiece>();
            whiteQueenMock.Setup(mock => mock.Color).Returns(PiecesColor.White);
            whiteQueenMock.Setup(mock => mock.ToString()).Returns("Q");

            var whiteKingMock = new Mock<IPiece>();
            whiteKingMock.Setup(mock => mock.Color).Returns(PiecesColor.White);
            whiteKingMock.Setup(mock => mock.ToString()).Returns("K");

            var pieceFactoryMock = new Mock<IPieceFactory>();
            pieceFactoryMock.Setup(mock => mock.CreatePawn(PiecesColor.Black))
                .Returns(blackPawnMock.Object);
            pieceFactoryMock.Setup(mock => mock.CreateBishop(PiecesColor.Black))
                .Returns(blackBishopMock.Object);
            pieceFactoryMock.Setup(mock => mock.CreateKnight(PiecesColor.Black))
                .Returns(blackKnightMock.Object);
            pieceFactoryMock.Setup(mock => mock.CreateRook(PiecesColor.Black))
                .Returns(blackRookMock.Object);
            pieceFactoryMock.Setup(mock => mock.CreateQueen(PiecesColor.Black))
                .Returns(blackQueenMock.Object);
            pieceFactoryMock.Setup(mock => mock.CreateKing(PiecesColor.Black))
                .Returns(blackKingMock.Object);
            pieceFactoryMock.Setup(mock => mock.CreatePawn(PiecesColor.White))
                .Returns(whitePawnMock.Object);
            pieceFactoryMock.Setup(mock => mock.CreateBishop(PiecesColor.White))
                .Returns(whiteBishopMock.Object);
            pieceFactoryMock.Setup(mock => mock.CreateKnight(PiecesColor.White))
                .Returns(whiteKnightMock.Object);
            pieceFactoryMock.Setup(mock => mock.CreateRook(PiecesColor.White))
                .Returns(whiteRookMock.Object);
            pieceFactoryMock.Setup(mock => mock.CreateQueen(PiecesColor.White))
                .Returns(whiteQueenMock.Object);
            pieceFactoryMock.Setup(mock => mock.CreateKing(PiecesColor.White))
                .Returns(whiteKingMock.Object);

            return (
                blackPawnMock,
                blackBishopMock,
                blackKnightMock,
                blackRookMock,
                blackQueenMock,
                blackKingMock,
                whitePawnMock,
                whiteBishopMock,
                whiteKnightMock,
                whiteRookMock,
                whiteQueenMock,
                whiteKingMock,
                pieceFactoryMock);
        }
    }
}
