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

            int ConvertCounterToIndex(int counter) =>
                frontColor == PiecesColor.Black
                    ? 8 - counter - 1
                    : counter;

            Console.WriteLine();

            for (var rowCounter = 0; rowCounter < 8; rowCounter++)
            {
                var rowIndex = ConvertCounterToIndex(rowCounter);

                Console.Write($"{8 - rowIndex} ");

                for (var colCounter = 0; colCounter < 8; colCounter++)
                {
                    var colIndex = ConvertCounterToIndex(colCounter);

                    if (board[rowIndex, colIndex].Piece is {} piece)
                    {
                        Console.ForegroundColor = piece.Color == PiecesColor.Black
                            ? blackPiecesColor
                            : whitePiecesColor;
                    }

                    Console.BackgroundColor = (rowIndex + colIndex) % 2 == 1
                        ? blackTilesColor
                        : whiteTilesColor;

                    Console.Write($" {board[rowIndex, colIndex]} ");
                }

                Console.ResetColor();
                Console.WriteLine();
            }

            Console.WriteLine(frontColor == PiecesColor.Black
                ? "   h  g  f  e  d  c  b  a"
                : "   a  b  c  d  e  f  g  h"
            );
        }

        public MoveSelection TryRequestMoveSelection(Board board, out Move? move)
        {
            Console.WriteLine();
            Console.WriteLine("move: e.g. \"a1b2\"");
            Console.WriteLine("save game: \"save\"");
            Console.WriteLine("exit game: \"exit\"");

            while (true)
            {
                Console.Write("command> ");

                move = null;

                switch (Console.ReadLine() ?? "")
                {
                    case var moveStr when Regex.IsMatch(moveStr, "[a-h][1-8][a-h][1-8]"):
                        int InvertIndex(int index) => 8 - index - 1;

                        var srcRow = InvertIndex(moveStr[1] - '1');
                        var srcCol = moveStr[0] - 'a';
                        var dstRow = InvertIndex(moveStr[3] - '1');
                        var dstCol = moveStr[2] - 'a';

                        switch (board.TryCreateMove(srcRow, srcCol, dstRow, dstCol, out move))
                        {
                            case CreateMoveResult.InvalidSrc:
                                Console.WriteLine("invalid source");
                                continue;

                            case CreateMoveResult.InvalidDst:
                                Console.WriteLine("invalid destination");
                                continue;

                            case CreateMoveResult.InvalidMove:
                                Console.WriteLine("invalid movement for piece");
                                continue;

                            case CreateMoveResult.InvalidPath:
                                Console.WriteLine("the path is obstructed");
                                continue;

                            case CreateMoveResult.Created:
                                return MoveSelection.Move;

                            default:
                                throw new NotImplementedException();
                        }

                    case "save":
                        return MoveSelection.SaveGame;

                    case "exit":
                        return MoveSelection.ExitGame;

                    default:
                        Console.WriteLine("invalid command");
                        continue;
                }
            }
        }
    }
}
