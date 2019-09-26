using System;
using Chess.Domain.Models;

namespace Chess.Services
{
    public class ConsoleService : IConsoleService
    {
        public void PrintBoard(Board board)
        {
            const ConsoleColor whitePiecesColor = ConsoleColor.White;
            const ConsoleColor blackPiecesColor = ConsoleColor.Black;

            const ConsoleColor whiteTilesColor = ConsoleColor.Gray;
            const ConsoleColor blackTilesColor = ConsoleColor.DarkGray;

            Console.WriteLine("   a  b  c  d  e  f  g  h");

            for (var row = 0; row < 8; row++)
            {
                Console.Write($"{8 - row} ");

                for (var col = 0; col < 8; col++)
                {
                    Console.ForegroundColor =
                        board[row, col].Piece?.Color == PiecesColor.White
                            ? whitePiecesColor
                            : blackPiecesColor;

                    Console.BackgroundColor = (row + col) % 2 == 0
                        ? whiteTilesColor
                        : blackTilesColor;

                    Console.Write($" {board[row, col]} ");
                }

                Console.ResetColor();

                Console.Write($" {8 - row}");
                Console.WriteLine();
            }

            Console.WriteLine("   a  b  c  d  e  f  g  h");
        }
    }
}
