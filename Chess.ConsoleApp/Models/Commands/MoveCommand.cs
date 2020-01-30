using System.Text.RegularExpressions;
using Chess.Domain.Models.Moves;

namespace Chess.ConsoleApp.Models.Commands
{
    public class MoveCommand : ICommand
    {
        public string Value { get; }

        public Move Move
        {
            get
            {
                var (srcRow, srcCol, dstRow, dstCol)
                    = (Value[1] - '1', Value[0] - 'a',
                        Value[3] - '1', Value[2] - 'a');

                return new Move(srcRow, srcCol, dstRow, dstCol);
            }
        }

        private MoveCommand(string value)
        {
            Value = value;
        }

        public static bool TryParse(string input, out MoveCommand? moveCommand)
        {
            const string movePattern = "[a-h][1-8][a-h][1-8]";

            if (Regex.IsMatch(input, movePattern))
            {
                moveCommand = new MoveCommand(input);
                return true;
            }

            moveCommand = null;
            return false;
        }
    }
}
