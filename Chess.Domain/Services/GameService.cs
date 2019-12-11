using System;
using System.Threading.Tasks;
using Chess.Domain.Enums;
using Chess.Domain.Models;
using Chess.Domain.Models.Players;
using Chess.Domain.Services.Interfaces;

namespace Chess.Domain.Services
{
    public class GameService : IGameService
    {
        private readonly IUserPlayerService _userPlayerService;
        private readonly ICpuPlayerService _cpuPlayerService;

        public GameService(IUserPlayerService userPlayerService, ICpuPlayerService cpuPlayerService)
        {
            _userPlayerService = userPlayerService;
            _cpuPlayerService = cpuPlayerService;
        }

        public async Task RunGame(Game game) =>
            await Task.Run(() =>
            {
                while (true)
                {
                    game.OnNewTurn(game.Board, game.TurnColor);

                    var player = game.TurnColor == PiecesColor.White
                        ? game.WhitePlayer
                        : game.BlackPlayer;

                    if (TryGetMove(player, game.Board, game.TurnColor, out var move))
                    {
                        // apply move here
                    }
                    else
                    {
                        break;
                    }
                }
            });

        private bool TryGetMove(IPlayer player, Board board, PiecesColor turnColor, out Move? move) =>
            player switch
            {
                UserPlayer userPlayer => _userPlayerService
                    .TryGetMove(userPlayer, board, turnColor, out move),
                CpuPlayer cpuPlayer => _cpuPlayerService
                    .TryGetMove(cpuPlayer, board, turnColor, out move),
                _ => throw new NotImplementedException()
            };
    }
}
