using Chess.Domain.Enums;
using Chess.Domain.Models.Games;

namespace Chess.Domain.Services
{
    public class GameFactory : IGameFactory
    {
        private readonly IBoardFactory _boardFactory;

        public GameFactory(IBoardFactory boardFactory)
        {
            _boardFactory = boardFactory;
        }

        public UserVsCpuGame CreateUserVsCpuGame(PiecesColor userColor, int recursionLevel)
        {
            return new UserVsCpuGame(_boardFactory, userColor, recursionLevel);
        }
    }
}
