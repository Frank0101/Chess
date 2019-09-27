using System;
using Chess.ConsoleApp.Enums;
using Chess.ConsoleApp.Models;
using Chess.Domain.Enums;
using Chess.Domain.Models;

namespace Chess.ConsoleApp.Services
{
    public class ConsoleService : IConsoleService
    {
        public void DisplayTitle()
        {
            Console.WriteLine(@"
   ____ _                     _   _      _   
  / ___| |__   ___  ___ ___  | \ | | ___| |_ 
 | |   | '_ \ / _ \/ __/ __| |  \| |/ _ \ __|
 | |___| | | |  __/\__ \__ \_| |\  |  __/ |_ 
  \____|_| |_|\___||___/___(_)_| \_|\___|\__|
            ");
        }

        public MainMenuSelection RequestMainMenuSelection() =>
            RequestKey("[n]ew game, [l]oad game") switch
            {
                'n' => MainMenuSelection.NewGame,
                'l' => MainMenuSelection.LoadGame,
                _ => RequestMainMenuSelection()
            };

        public NewGameConfig RequestNewGameConfig() =>
            new NewGameConfig(
                RequestUserColor(),
                RequestRecursionLevel()
            );

        private static PiecesColor RequestUserColor() =>
            RequestKey("[b]lack pieces, [w]hite pieces") switch
            {
                'b' => PiecesColor.Black,
                'w' => PiecesColor.White,
                _ => RequestUserColor()
            };

        private static int RequestRecursionLevel() =>
            RequestKey("recursion level (3 suggested)") switch
            {
                var key when int.TryParse(key.ToString(), out var level) && level > 0 => level,
                _ => RequestRecursionLevel()
            };

        private static char RequestKey(string prompt)
        {
            Console.Write($"{prompt}: ");
            var key = Console.ReadKey().KeyChar;
            Console.WriteLine();
            return key;
        }

        public void DisplayBoard(Board board, PiecesColor frontColor)
        {
            const ConsoleColor blackPiecesColor = ConsoleColor.Black;
            const ConsoleColor whitePiecesColor = ConsoleColor.White;

            const ConsoleColor blackTilesColor = ConsoleColor.DarkGray;
            const ConsoleColor whiteTilesColor = ConsoleColor.Gray;

            for (var rowIndex = 0; rowIndex < 8; rowIndex++)
            {
                var row = frontColor == PiecesColor.Black ? 8 - rowIndex - 1 : rowIndex;

                Console.Write($"{8 - row}  ");

                for (var colIndex = 0; colIndex < 8; colIndex++)
                {
                    var col = frontColor == PiecesColor.Black ? 8 - colIndex - 1 : colIndex;

                    Console.ForegroundColor =
                        board[row, col].Piece?.Color == PiecesColor.Black
                            ? blackPiecesColor
                            : whitePiecesColor;

                    Console.BackgroundColor = (row + col) % 2 == 1
                        ? blackTilesColor
                        : whiteTilesColor;

                    Console.Write($" {board[row, col]} ");
                }

                Console.ResetColor();
                Console.WriteLine();
            }

            var colsLabel = frontColor == PiecesColor.Black
                ? "h  g  f  e  d  c  b  a"
                : "a  b  c  d  e  f  g  h";

            Console.WriteLine($"   {colsLabel}");
        }
    }
}
