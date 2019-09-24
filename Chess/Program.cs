using Chess.Models;
using Chess.Services;

namespace Chess
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var board = new Board(PiecesColor.White);
            var boardDisplayService = new BoardDisplayService();
            boardDisplayService.DisplayBoard(board);
        }
    }
}
