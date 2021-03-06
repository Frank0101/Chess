using System;
using System.Threading.Tasks;
using Chess.Domain.Enums;
using Chess.Domain.Models;
using Chess.Domain.Models.Moves;
using Chess.Domain.Models.Players;
using Chess.Domain.Services.Interfaces;

namespace Chess.Domain.Services
{
    public class GameService : IGameService
    {
        private readonly IUserPlayerService _userPlayerService;
        private readonly ICpuPlayerService _cpuPlayerService;

        public GameService(
            IUserPlayerService userPlayerService,
            ICpuPlayerService cpuPlayerService)
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

                    var move = GetMove(player, game.Board, game.TurnColor);
                    if (move != null)
                    {
                        if (game.OnMoveConfirm(game.Board, game.TurnColor, move))
                        {
                            game.Board.ApplyMove(move);
                        }
                        else
                        {
                            continue;
                        }
                    }
                    else
                    {
                        break;
                    }

                    game.TurnColor = game.TurnColor.Invert();
                }
            });

        private Move? GetMove(IPlayer player, Board board, PiecesColor turnColor) =>
            player switch
            {
                UserPlayer userPlayer => _userPlayerService.GetMove(userPlayer, board, turnColor),
                CpuPlayer cpuPlayer => _cpuPlayerService.GetMove(cpuPlayer, board, turnColor),
                _ => throw new NotImplementedException()
            };
    }
}
