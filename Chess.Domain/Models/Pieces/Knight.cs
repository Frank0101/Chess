using Chess.Domain.Enums;

namespace Chess.Domain.Models.Pieces
{
    public class Knight : Piece
    {
        public Knight(PiecesColor color)
            : base(color, color == PiecesColor.Black ? 'n' : 'N', 4)
        {
        }

        public override bool IsMoveValid(MoveDescriptor moveDescriptor)
        {
            return true;
        }
    }
}
