using System.Linq;
using System.Text.RegularExpressions;
using Chess.Domain.Models.Moves;

namespace Chess.ConsoleApp.Models.Commands
{
    public class MoveCommand : ICommand
    {
        private const string MovePattern = "[a-h][1-8][a-h][1-8]";

        public string Value { get; }

        public Move Move
        {
            get
            {
                static Move CreateMove(string value)
                {
                    var (srcRow, srcCol, dstRow, dstCol)
                        = (value[1] - '1', value[0] - 'a',
                            value[3] - '1', value[2] - 'a');

                    return new Move(srcRow, srcCol, dstRow, dstCol);
                }

                Move? head = null, current = null;
                var matches = Regex.Matches(Value, MovePattern);

                foreach (var value in matches.Select(match => match.Value))
                {
                    if (head == null)
                    {
                        head = CreateMove(value);
                        current = head;
                    }
                    else if (current != null)
                    {
                        current.Next = CreateMove(value);
                        current = current.Next;
                    }
                }

                return head!;
            }
        }

        private MoveCommand(string value)
        {
            Value = value;
        }

        public static bool TryParse(string input, out MoveCommand? moveCommand)
        {
            var movesPattern = $"^{MovePattern}(\\+{MovePattern})?$";

            if (Regex.IsMatch(input, movesPattern))
            {
                moveCommand = new MoveCommand(input);
                return true;
            }

            moveCommand = null;
            return false;
        }
    }
}
