using System;
using Chess.Console.Enums;
using Chess.Console.Models;
using Chess.Domain.Enums;
using Chess.Domain.Models;
using console = System.Console;

namespace Chess.Console.Services
{
    public class ConsoleService : IConsoleService
    {
        public void PrintTitle()
        {
            console.WriteLine(@"
   ____ _                     _   _      _   
  / ___| |__   ___  ___ ___  | \ | | ___| |_ 
 | |   | '_ \ / _ \/ __/ __| |  \| |/ _ \ __|
 | |___| | | |  __/\__ \__ \_| |\  |  __/ |_ 
  \____|_| |_|\___||___/___(_)_| \_|\___|\__|
            ");
        }

        public MainMenuOption GetMainMenuSelection() =>
            RequestKey("[n]ew game, [l]oad game") switch
            {
                'n' => MainMenuOption.NewGame,
                'l' => MainMenuOption.LoadGame,
                _ => GetMainMenuSelection()
            };

        public NewGameConfig GetNewGameMenuSelection() =>
            new NewGameConfig(
                GetUserColorSelection(),
                GetRecursionLevelSelection()
            );

        private static PiecesColor GetUserColorSelection() =>
            RequestKey("[b]lack pieces, [w]hite pieces") switch
            {
                'b' => PiecesColor.Black,
                'w' => PiecesColor.White,
                _ => GetUserColorSelection()
            };

        private static int GetRecursionLevelSelection() =>
            RequestKey("recursion level (3 suggested)") switch
            {
                var key when int.TryParse(key.ToString(), out var level) => level,
                _ => GetRecursionLevelSelection()
            };

        private static char RequestKey(string prompt)
        {
            console.Write($"{prompt}: ");
            var key = console.ReadKey().KeyChar;
            console.WriteLine();
            return key;
        }

        public void PrintBoard(Board board)
        {
            const ConsoleColor whitePiecesColor = ConsoleColor.White;
            const ConsoleColor blackPiecesColor = ConsoleColor.Black;

            const ConsoleColor whiteTilesColor = ConsoleColor.Gray;
            const ConsoleColor blackTilesColor = ConsoleColor.DarkGray;

            console.WriteLine("   a  b  c  d  e  f  g  h");

            for (var row = 0; row < 8; row++)
            {
                console.Write($"{8 - row} ");

                for (var col = 0; col < 8; col++)
                {
                    console.ForegroundColor =
                        board[row, col].Piece?.Color == PiecesColor.White
                            ? whitePiecesColor
                            : blackPiecesColor;

                    console.BackgroundColor = (row + col) % 2 == 0
                        ? whiteTilesColor
                        : blackTilesColor;

                    console.Write($" {board[row, col]} ");
                }

                console.ResetColor();

                console.Write($" {8 - row}");
                console.WriteLine();
            }

            console.WriteLine("   a  b  c  d  e  f  g  h");
        }
    }
}
