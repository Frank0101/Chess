using Chess.ConsoleApp.Enums;
using Chess.ConsoleApp.Models;
using Chess.Domain.Enums;
using Chess.Domain.Models;

namespace Chess.ConsoleApp.Services
{
    public interface IConsoleService
    {
        void DisplayTitle();
        MainMenuSelection RequestMainMenuSelection();
        NewGameConfig RequestNewGameConfig();
        void DisplayBoard(Board board, PiecesColor frontColor);
        MoveSelection TryRequestMoveSelection(Board board, out Move? move);
    }
}
