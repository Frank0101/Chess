using Chess.ConsoleApp.Enums;
using Chess.ConsoleApp.Models;
using Chess.ConsoleApp.Models.Commands;
using Chess.Domain.Enums;
using Chess.Domain.Models;
using Chess.Domain.Models.Moves;

namespace Chess.ConsoleApp.Services.Interfaces
{
    public interface IConsoleService
    {
        void DisplayTitle();
        MainMenuSelection GetMainMenuSelection();
        NewGameConfig GetNewGameConfig();
        void DisplayBoard(Board board, PiecesColor frontColor, Move? move = null);
        void DisplayCommandsMenu();
        ICommand GetCommand();
        void DisplayMoveValidationResult(MoveValidationResult validationResult);
        bool GetMoveConfirmation();
    }
}
