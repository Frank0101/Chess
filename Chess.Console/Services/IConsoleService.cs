using Chess.Console.Enums;
using Chess.Console.Models;
using Chess.Domain.Models;

namespace Chess.Console.Services
{
    public interface IConsoleService
    {
        void PrintTitle();
        MainMenuOption GetMainMenuSelection();
        NewGameConfig GetNewGameMenuSelection();
        void PrintBoard(Board board);
    }
}
