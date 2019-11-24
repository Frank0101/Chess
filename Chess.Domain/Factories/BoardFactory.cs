using Chess.Domain.Models;
using Chess.Domain.Services;

namespace Chess.Domain.Factories
{
    public class BoardFactory : IBoardFactory
    {
        private readonly IMoveValidationService _moveValidationService;

        public BoardFactory(IMoveValidationService moveValidationService)
        {
            _moveValidationService = moveValidationService;
        }

        public Board Create()
        {
            return new Board(_moveValidationService);
        }
    }
}
