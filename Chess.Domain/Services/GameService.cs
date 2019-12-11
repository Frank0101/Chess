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

                    var turnPlayer = game.TurnColor == PiecesColor.White
                        ? game.WhitePlayer
                        : game.BlackPlayer;

                    var move = turnPlayer switch
                    {
                        UserPlayer userPlayer when _userPlayerService
                            .TryGetMove(userPlayer, game.Board, game.TurnColor, out var m) => m,
                        CpuPlayer cpuPlayer when _cpuPlayerService
                            .TryGetMove(cpuPlayer, game.Board, game.TurnColor, out var m) => m,
                        _ => throw new NotImplementedException()
                    };

                    // apply move here
                    break;
                }
            });
    }
}
