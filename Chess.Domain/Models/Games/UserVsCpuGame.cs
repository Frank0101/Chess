using System;
using Chess.Domain.Enums;
using Chess.Domain.Models.Players;
using Chess.Domain.services;

namespace Chess.Domain.Models.Games
{
    public class UserVsCpuGame : Game
    {
        public UserPlayer UserPlayer =>
            Player1 as UserPlayer ?? throw new InvalidCastException();

        public CpuPlayer CpuPlayer =>
            Player2 as CpuPlayer ?? throw new InvalidCastException();

        public UserVsCpuGame(IBoardFactory boardFactory, PiecesColor userColor, int recursionLevel)
            : base(
                boardFactory,
                new UserPlayer(userColor),
                new CpuPlayer(userColor.Invert(), recursionLevel))
        {
        }
    }
}
