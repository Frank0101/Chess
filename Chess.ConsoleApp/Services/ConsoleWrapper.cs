using System;
using Chess.ConsoleApp.Services.Interfaces;

namespace Chess.ConsoleApp.Services
{
    public class ConsoleWrapper : IConsoleWrapper
    {
        public void Write(string value) =>
            Console.Write(value);

        public void WriteLine(string value = "") =>
            Console.WriteLine(value);

        public char ReadKey() =>
            Console.ReadKey().KeyChar;

        public string ReadLine() =>
            Console.ReadLine();

        public void SetForegroundColor(ConsoleColor color) =>
            Console.ForegroundColor = color;

        public void SetBackgroundColor(ConsoleColor color) =>
            Console.BackgroundColor = color;

        public void ResetColor() =>
            Console.ResetColor();
    }
}
