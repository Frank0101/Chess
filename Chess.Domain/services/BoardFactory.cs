using Chess.Domain.Models;

namespace Chess.Domain.Services
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