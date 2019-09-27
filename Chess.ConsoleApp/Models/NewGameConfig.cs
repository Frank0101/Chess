using Chess.Domain.Enums;

namespace Chess.ConsoleApp.Models
{
    public class NewGameConfig
    {
        public PiecesColor UserColor { get; }
        public int RecursionLevel { get; }

        public NewGameConfig(PiecesColor userColor, int recursionLevel)
        {
            UserColor = userColor;
            RecursionLevel = recursionLevel;
        }
    }
}
