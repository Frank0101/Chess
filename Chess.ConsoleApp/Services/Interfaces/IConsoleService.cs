using Chess.ConsoleApp.Enums;
using Chess.ConsoleApp.Models;
using Chess.ConsoleApp.Models.Commands;
using Chess.Domain.Enums;
using Chess.Domain.Models;

namespace Chess.ConsoleApp.Services.Interfaces
{
    public interface IConsoleService
    {
        void DisplayTitle();
        MainMenuSelection GetMainMenuSelection();
        NewGameConfig GetNewGameConfig();
        void DisplayBoard(Board board, PiecesColor frontColor);
        void DisplayCommandsMenu();
        ICommand GetCommand();
        void DisplayMoveValidationResult(Move? move, MoveValidationResult validationResult);
    }
}
