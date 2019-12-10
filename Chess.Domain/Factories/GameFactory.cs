using Chess.Domain.Enums;
using Chess.Domain.Models.Games;

namespace Chess.Domain.Factories
{
    public class GameFactory : IGameFactory
    {
        private readonly IBoardFactory _boardFactory;
        private readonly IPlayerFactory _playerFactory;

        public GameFactory(IBoardFactory boardFactory, IPlayerFactory playerFactory)
        {
            _boardFactory = boardFactory;
            _playerFactory = playerFactory;
        }

        public IUserVsCpuGame CreateUserVsCpuGame(PiecesColor userColor, int recursionLevel)
        {
            return new UserVsCpuGame(_boardFactory, _playerFactory, userColor, recursionLevel);
        }
    }
}
