using Chess.Domain.Enums;
using Chess.Domain.Models.Players;

namespace Chess.Domain.Models.Games
{
    public class UserVsCpuGame : Game
    {
        public UserPlayer UserPlayer => Player1 as UserPlayer;
        public CpuPlayer CpuPlayer => Player2 as CpuPlayer;

        public UserVsCpuGame(PiecesColor userColor, int recursionLevel)
            : base(
                new UserPlayer(userColor),
                new CpuPlayer(userColor.Invert(), recursionLevel))
        {
        }
    }
}
