using Chess.Domain.Enums;

namespace Chess.Domain.Models.Players
{
    public class UserPlayer : Player
    {
        public UserPlayer(PiecesColor color) : base(color)
        {
        }
    }
}
