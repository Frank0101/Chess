using System;
using System.Text.RegularExpressions;
using Chess.ConsoleApp.Enums;
using Chess.ConsoleApp.Models;
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

        public void DisplayTitle()
        {
            _consoleWrapper.WriteLine(@"
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

        public NewGameConfig RequestNewGameConfig()
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
            const ConsoleColor blackPiecesColor = ConsoleColor.Black;
            const ConsoleColor whitePiecesColor = ConsoleColor.White;

            const ConsoleColor blackTilesColor = ConsoleColor.DarkGray;
            const ConsoleColor whiteTilesColor = ConsoleColor.Gray;

            int ConvertCounterToIndex(int counter) =>
                frontColor == PiecesColor.Black
                    ? 8 - counter - 1
                    : counter;

            _consoleWrapper.WriteLine();

            for (var rowCounter = 0; rowCounter < 8; rowCounter++)
            {
                var rowIndex = ConvertCounterToIndex(rowCounter);

                _consoleWrapper.Write($"{8 - rowIndex} ");

                for (var colCounter = 0; colCounter < 8; colCounter++)
                {
                    var colIndex = ConvertCounterToIndex(colCounter);

                    if (board[rowIndex, colIndex].Piece is {} piece)
                    {
                        _consoleWrapper.SetForegroundColor(piece.Color == PiecesColor.Black
                            ? blackPiecesColor
                            : whitePiecesColor);
                    }

                    _consoleWrapper.SetBackgroundColor((rowIndex + colIndex) % 2 == 1
                        ? blackTilesColor
                        : whiteTilesColor);

                    _consoleWrapper.Write($" {board[rowIndex, colIndex]} ");
                }

                _consoleWrapper.ResetColor();
                _consoleWrapper.WriteLine();
            }

            _consoleWrapper.WriteLine(frontColor == PiecesColor.Black
                ? "   h  g  f  e  d  c  b  a"
                : "   a  b  c  d  e  f  g  h"
            );
        }

        public MoveSelection TryRequestMoveSelection(Board board, out Move? move)
        {
            _consoleWrapper.WriteLine();
            _consoleWrapper.WriteLine("move: e.g. \"a1b2\"");
            _consoleWrapper.WriteLine("save game: \"save\"");
            _consoleWrapper.WriteLine("exit game: \"exit\"");

            while (true)
            {
                _consoleWrapper.Write("command> ");

                move = null;

                switch (_consoleWrapper.ReadLine() ?? "")
                {
                    case var moveStr when Regex.IsMatch(moveStr, "[a-h][1-8][a-h][1-8]"):
                        int InvertIndex(int index) => 8 - index - 1;

                        var srcRow = InvertIndex(moveStr[1] - '1');
                        var srcCol = moveStr[0] - 'a';
                        var dstRow = InvertIndex(moveStr[3] - '1');
                        var dstCol = moveStr[2] - 'a';

                        var moveDescriptor = new MoveDescriptor(srcRow, srcCol, dstRow, dstCol);

                        switch (board.TryCreateMove(moveDescriptor, out move))
                        {
                            case MoveValidationResult.InvalidSrc:
                                _consoleWrapper.WriteLine("invalid source");
                                continue;

                            case MoveValidationResult.InvalidDst:
                                _consoleWrapper.WriteLine("invalid destination");
                                continue;

                            case MoveValidationResult.InvalidMove:
                                _consoleWrapper.WriteLine("invalid movement for piece");
                                continue;

                            case MoveValidationResult.InvalidPath:
                                _consoleWrapper.WriteLine("the path is obstructed");
                                continue;

                            case MoveValidationResult.Valid:
                                return MoveSelection.Move;

                            default:
                                throw new NotImplementedException();
                        }

                    case "save":
                        return MoveSelection.SaveGame;

                    case "exit":
                        return MoveSelection.ExitGame;

                    default:
                        _consoleWrapper.WriteLine("invalid command");
                        continue;
                }
            }
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
