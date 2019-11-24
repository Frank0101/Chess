using Chess.Domain.Enums;
using Chess.Domain.Factories;
using Chess.Domain.Models.Pieces;
using FluentAssertions;
using Xunit;

namespace Chess.Test.Unit.Domain.Factories
{
    public class PieceFactoryTest
    {
        [Fact]
        public void CreatePawn_ShouldCreatePawn()
        {
            // arrange
            var sut = new PieceFactory();

            // act
            var pawn = sut.CreatePawn(PiecesColor.Black);

            // assert
            pawn.Should().BeOfType<Pawn>();
        }

        [Fact]
        public void CreateBishop_ShouldCreateBishop()
        {
            // arrange
            var sut = new PieceFactory();

            // act
            var bishop = sut.CreateBishop(PiecesColor.Black);

            // assert
            bishop.Should().BeOfType<Bishop>();
        }

        [Fact]
        public void CreateKnight_ShouldCreateKnight()
        {
            // arrange
            var sut = new PieceFactory();

            // act
            var knight = sut.CreateKnight(PiecesColor.Black);

            // assert
            knight.Should().BeOfType<Knight>();
        }

        [Fact]
        public void CreateRook_ShouldCreateRook()
        {
            // arrange
            var sut = new PieceFactory();

            // act
            var rook = sut.CreateRook(PiecesColor.Black);

            // assert
            rook.Should().BeOfType<Rook>();
        }

        [Fact]
        public void CreateQueen_ShouldCreateQueen()
        {
            // arrange
            var sut = new PieceFactory();

            // act
            var queen = sut.CreateQueen(PiecesColor.Black);

            // assert
            queen.Should().BeOfType<Queen>();
        }

        [Fact]
        public void CreateKing_ShouldCreateKing()
        {
            // arrange
            var sut = new PieceFactory();

            // act
            var king = sut.CreateKing(PiecesColor.Black);

            // assert
            king.Should().BeOfType<King>();
        }
    }
}
