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
        public void CreatePawn_GivenColor_ShouldCreatePawn()
        {
            // arrange
            var sut = new PieceFactory();

            // act
            var pawn = sut.CreatePawn(PiecesColor.Black);

            // assert
            pawn.Should().BeOfType<Pawn>();
            pawn.Color.Should().Be(PiecesColor.Black);
        }

        [Fact]
        public void CreateBishop_GivenColor_ShouldCreateBishop()
        {
            // arrange
            var sut = new PieceFactory();

            // act
            var bishop = sut.CreateBishop(PiecesColor.Black);

            // assert
            bishop.Should().BeOfType<Bishop>();
            bishop.Color.Should().Be(PiecesColor.Black);
        }

        [Fact]
        public void CreateKnight_GivenColor_ShouldCreateKnight()
        {
            // arrange
            var sut = new PieceFactory();

            // act
            var knight = sut.CreateKnight(PiecesColor.Black);

            // assert
            knight.Should().BeOfType<Knight>();
            knight.Color.Should().Be(PiecesColor.Black);
        }

        [Fact]
        public void CreateRook_GivenColor_ShouldCreateRook()
        {
            // arrange
            var sut = new PieceFactory();

            // act
            var rook = sut.CreateRook(PiecesColor.Black);

            // assert
            rook.Should().BeOfType<Rook>();
            rook.Color.Should().Be(PiecesColor.Black);
        }

        [Fact]
        public void CreateQueen_GivenColor_ShouldCreateQueen()
        {
            // arrange
            var sut = new PieceFactory();

            // act
            var queen = sut.CreateQueen(PiecesColor.Black);

            // assert
            queen.Should().BeOfType<Queen>();
            queen.Color.Should().Be(PiecesColor.Black);
        }

        [Fact]
        public void CreateKing_GivenColor_ShouldCreateKing()
        {
            // arrange
            var sut = new PieceFactory();

            // act
            var king = sut.CreateKing(PiecesColor.Black);

            // assert
            king.Should().BeOfType<King>();
            king.Color.Should().Be(PiecesColor.Black);
        }
    }
}
