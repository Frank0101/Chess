using System;
using System.Collections.Generic;
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
            _consoleWrapper.WriteLine("Chess - F.C. 2020");

        public MainMenuSelection GetMainMenuSelection()
        {
            MainMenuSelection GetMainMenuNewGameSelection() =>
                RequestKey("[u]ser vs cpu, [U]ser vs user, [c]pu vs cpu") switch
                {
                    'u' => MainMenuSelection.NewUserVsCpuGame,
                    'U' => MainMenuSelection.NewUserVsUserGame,
                    'c' => MainMenuSelection.NewCpuVsCpuGame,
                    _ => GetMainMenuNewGameSelection()
                };

            return RequestKey("[n]ew game, [l]oad game") switch
            {
                'n' => GetMainMenuNewGameSelection(),
                'l' => MainMenuSelection.LoadGame,
                _ => GetMainMenuSelection()
            };
        }

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

                    var pieceStr = board[row, col]?.ToString()?.ToUpper() ?? " ";
                    if (move != null && (row, col) == move.Src)
                    {
                        _consoleWrapper.SetForegroundColor(srcMoveColor);
                        _consoleWrapper.Write($" {pieceStr} ");
                    }
                    else if (move != null && (row, col) == move.Dst)
                    {
                        _consoleWrapper.SetForegroundColor(dstMoveColor);
                        _consoleWrapper.Write($" * ");
                    }
                    else if (board[row, col] is {} piece)
                    {
                        _consoleWrapper.SetForegroundColor(piece.Color == PiecesColor.White
                            ? whitePiecesColor
                            : blackPiecesColor);

                        _consoleWrapper.Write($" {pieceStr} ");
                    }
                    else
                    {
                        _consoleWrapper.Write("   ");
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
                MoveValidationResult.KingUnderCheck => "king would be under check",
                MoveValidationResult.CastlingRookNotInPosition => "no rook in position for castling",
                MoveValidationResult.CastlingPiecesAlreadyMoved => "castling pieces already moved",
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

        public void DisplayBranchComputed(int recursionLevel,
            List<CpuMove> moves, CpuMove bestMove, TimeSpan time)
        {
            switch (recursionLevel)
            {
                case 0:
                    _consoleWrapper.WriteLine();
                    foreach (var move in moves)
                    {
                        if (move == bestMove)
                        {
                            _consoleWrapper.SetForegroundColor(ConsoleColor.Red);
                        }
                        else if (move.Value == bestMove.Value)
                        {
                            _consoleWrapper.SetForegroundColor(ConsoleColor.Yellow);
                        }

                        _consoleWrapper.WriteLine(move.ToString());
                        _consoleWrapper.ResetColor();
                    }

                    _consoleWrapper.WriteLine($"elapsed time: {time.TotalSeconds}");
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
