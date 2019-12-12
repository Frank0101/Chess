using System;
using Chess.ConsoleApp.Enums;
using Chess.ConsoleApp.Models;
using Chess.ConsoleApp.Services.Interfaces;
using Chess.Domain.Enums;
using Chess.Domain.Models;

namespace Chess.ConsoleApp.Services
{
    public class ConsoleService : IConsoleService
    {
        private readonly IConsoleWrapper _consoleWrapper;

        public ConsoleService(IConsoleWrapper consoleWrapper)
        {
            _consoleWrapper = consoleWrapper;
        }

        public void DisplayTitle() =>
            _consoleWrapper.WriteLine("Chess - F.C. 2019");

        public MainMenuSelection GetMainMenuSelection() =>
            RequestKey("[n]ew game, [l]oad game") switch
            {
                'n' => MainMenuSelection.NewGame,
                'l' => MainMenuSelection.LoadGame,
                _ => GetMainMenuSelection()
            };

        public NewGameConfig GetNewGameConfig()
        {
            PiecesColor RequestUserColor() =>
                RequestKey("[b]lack pieces, [w]hite pieces") switch
                {
                    'b' => PiecesColor.Black,
                    'w' => PiecesColor.White,
                    _ => RequestUserColor()
                };

            int RequestRecursionLevel() =>
                RequestKey("recursion level (3 suggested)") switch
                {
                    var key when int.TryParse(key.ToString(), out var level) && level > 0 => level,
                    _ => RequestRecursionLevel()
                };

            return new NewGameConfig(
                RequestUserColor(),
                RequestRecursionLevel()
            );
        }

        public void DisplayBoard(Board board, PiecesColor frontColor)
        {
            const ConsoleColor whitePiecesColor = ConsoleColor.White;
            const ConsoleColor blackPiecesColor = ConsoleColor.Black;

            const ConsoleColor whiteTilesColor = ConsoleColor.Gray;
            const ConsoleColor blackTilesColor = ConsoleColor.DarkGray;

            var (startRow, endRow, rowInc, startCol, endCol, colInc) = frontColor == PiecesColor.White
                ? (7, -1, -1, 0, 8, 1)
                : (0, 8, 1, 7, -1, -1);

            for (var row = startRow; row != endRow; row += rowInc)
            {
                _consoleWrapper.Write($"{row + 1} ");

                for (var col = startCol; col != endCol; col += colInc)
                {
                    if (board[row, col] is {} piece)
                    {
                        _consoleWrapper.SetForegroundColor(piece.Color == PiecesColor.White
                            ? whitePiecesColor
                            : blackPiecesColor);
                    }

                    _consoleWrapper.SetBackgroundColor((row + col) % 2 == 0
                        ? whiteTilesColor
                        : blackTilesColor);

                    _consoleWrapper.Write($" {board[row, col]?.ToString() ?? " "} ");
                }

                _consoleWrapper.ResetColor();
                _consoleWrapper.WriteLine();
            }

            _consoleWrapper.WriteLine(frontColor == PiecesColor.White
                ? "   a  b  c  d  e  f  g  h"
                : "   h  g  f  e  d  c  b  a"
            );
        }

        private char RequestKey(string prompt)
        {
            _consoleWrapper.Write($"{prompt}: ");
            var key = _consoleWrapper.ReadKey();
            _consoleWrapper.WriteLine();
            return key;
        }
    }
}
