using Chess.Domain.Enums;

namespace Chess.ConsoleApp.Models
{
    public class NewGameConfig
    {
        public PiecesColor UserColor { get; }
        public int RecursionDepth { get; }

        public NewGameConfig(PiecesColor userColor, int recursionDepth)
        {
            UserColor = userColor;
            RecursionDepth = recursionDepth;
        }

        public void Deconstruct(out PiecesColor userColor, out int recursionDepth)
        {
            userColor = UserColor;
            recursionDepth = RecursionDepth;
        }
    }
}
