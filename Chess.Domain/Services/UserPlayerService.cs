using System;
using Chess.Domain.Enums;
using Chess.Domain.Models;
using Chess.Domain.Models.Players;
using Chess.Domain.Services.Interfaces;

namespace Chess.Domain.Services
{
    public class UserPlayerService : IUserPlayerService
    {
        public bool TryGetMove(UserPlayer player, Board board, PiecesColor turnColor, out Move? move)
        {
            throw new NotImplementedException();
        }
    }
}
