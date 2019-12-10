using System;
using Chess.Domain.Enums;
using Chess.Domain.Factories;
using Chess.Domain.Models.Players;

namespace Chess.Domain.Models.Games
{
    public class UserVsCpuGame : Game, IUserVsCpuGame
    {
        public IUserPlayer UserPlayer =>
            Player1 as IUserPlayer ?? throw new InvalidCastException();

        public ICpuPlayer CpuPlayer =>
            Player2 as ICpuPlayer ?? throw new InvalidCastException();

        public UserVsCpuGame(IBoardFactory boardFactory, IPlayerFactory playerFactory,
            PiecesColor userColor, int recursionLevel)
            : base(
                boardFactory,
                playerFactory.CreateUserPlayer(userColor),
                playerFactory.CreateCpuPlayer(userColor.Invert(), recursionLevel))
        {
        }
    }
}
