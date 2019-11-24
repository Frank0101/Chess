using System;

namespace Chess.ConsoleApp.Services
{
    public interface IConsoleWrapper
    {
        void Write(string value);
        void WriteLine(string value = "");
        char ReadKey();
        string ReadLine();
        void SetForegroundColor(ConsoleColor color);
        void SetBackgroundColor(ConsoleColor color);
        void ResetColor();
    }
}
