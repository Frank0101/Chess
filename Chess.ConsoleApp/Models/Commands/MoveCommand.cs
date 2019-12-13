namespace Chess.ConsoleApp.Models.Commands
{
    public class MoveCommand : ICommand
    {
        public string Value { get; }

        public MoveCommand(string value)
        {
            Value = value;
        }
    }
}
