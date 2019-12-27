using System;
using Chess.ConsoleApp.Enums;
using Chess.ConsoleApp.Models;
using Chess.ConsoleApp.Models.Commands;
using Chess.ConsoleApp.Services.Interfaces;
using Chess.Domain.Enums;
using Chess.Domain.Models;
using Chess.Domain.Models.Moves;

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
            PiecesColor GetUserColor() =>
                RequestKey("[w]hite pieces, [b]lack pieces") switch
                {
                    'w' => PiecesColor.White,
                    'b' => PiecesColor.Black,
                    _ => GetUserColor()
                };

            int GetRecursionDepth() =>
                RequestKey("recursion depth (3 suggested)") switch
                {
                    var key when int.TryParse(key.ToString(), out var level) && level > 0 => level,
                    _ => GetRecursionDepth()
                };

            return new NewGameConfig(
                GetUserColor(),
                GetRecursionDepth()
            );
        }

        public void DisplayBoard(Board board, PiecesColor frontColor, Move? move = null)
        {
            const ConsoleColor
                whitePiecesColor = ConsoleColor.White,
                blackPiecesColor = ConsoleColor.Black,
                whiteTilesColor = ConsoleColor.Gray,
                blackTilesColor = ConsoleColor.DarkGray,
                srcMoveColor = ConsoleColor.Red,
                dstMoveColor = ConsoleColor.DarkRed;

            var (startRow, endRow, rowInc, startCol, endCol, colInc) = frontColor == PiecesColor.White
                ? (7, -1, -1, 0, 8, 1)
                : (0, 8, 1, 7, -1, -1);

            for (var row = startRow; row != endRow; row += rowInc)
            {
                _consoleWrapper.Write($"{row + 1} ");

                for (var col = startCol; col != endCol; col += colInc)
                {
                    _consoleWrapper.SetBackgroundColor((row + col) % 2 == 0
                        ? whiteTilesColor
                        : blackTilesColor);

                    if (move != null && (row, col) == move.Src)
                    {
                        _consoleWrapper.SetForegroundColor(srcMoveColor);
                        _consoleWrapper.Write($" {board[row, col]?.ToString() ?? " "} ");
                    }
                    else if (move != null && (row, col) == move.Dst)
                    {
                        _consoleWrapper.SetForegroundColor(dstMoveColor);
                        _consoleWrapper.Write($" * ");
                    }
                    else
                    {
                        if (board[row, col] is {} piece)
                        {
                            _consoleWrapper.SetForegroundColor(piece.Color == PiecesColor.White
                                ? whitePiecesColor
                                : blackPiecesColor);
                        }

                        _consoleWrapper.Write($" {board[row, col]?.ToString() ?? " "} ");
                    }
                }

                _consoleWrapper.ResetColor();
                _consoleWrapper.WriteLine();
            }

            _consoleWrapper.WriteLine(frontColor == PiecesColor.White
                ? "   a  b  c  d  e  f  g  h"
                : "   h  g  f  e  d  c  b  a"
            );
        }

        public void DisplayCommandsMenu()
        {
            _consoleWrapper.WriteLine("move: e.g. \"a1b2\"");
            _consoleWrapper.WriteLine("save game: \"save\"");
            _consoleWrapper.WriteLine("quit game: \"quit\"");
        }

        public ICommand GetCommand()
        {
            _consoleWrapper.Write("command> ");

            return _consoleWrapper.ReadLine() switch
            {
                var input when MoveCommand.TryParse(input, out var moveCommand) => moveCommand!,
                "save" => new SaveCommand(),
                "quit" => new QuitCommand(),
                _ => GetCommand()
            };
        }

        public void DisplayMoveValidationResult(MoveValidationResult validationResult)
        {
            _consoleWrapper.WriteLine(validationResult switch
            {
                MoveValidationResult.InvalidSrc => "invalid source",
                MoveValidationResult.InvalidDst => "invalid destination",
                MoveValidationResult.InvalidMove => "piece can't move that way",
                MoveValidationResult.InvalidPath => "piece can't jump",
                MoveValidationResult.Valid => "move valid",
                _ => throw new NotImplementedException()
            });
        }

        public bool GetMoveConfirmation() =>
            RequestKey("confirm move? [y/n]") switch
            {
                'y' => true,
                'n' => false,
                (char) ConsoleKey.Enter => true,
                _ => GetMoveConfirmation()
            };

        public void DisplayBranchComputed(int recursionLevel)
        {
            switch (recursionLevel)
            {
                case 0:
                    _consoleWrapper.WriteLine();
                    break;
                case 1:
                    _consoleWrapper.Write(".");
                    break;
            }
        }

        public void WaitMoveAcknowledge() =>
            RequestKey("press any key");

        private char RequestKey(string prompt)
        {
            _consoleWrapper.Write($"{prompt}: ");
            var key = _consoleWrapper.ReadKey();
            _consoleWrapper.WriteLine();
            return key;
        }
    }
}
