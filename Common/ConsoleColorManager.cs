using System;

namespace Common
{
    public class ConsoleColorManager : IDisposable
    {
        private readonly ConsoleColor _previousForegroundColor;
        private readonly ConsoleColor _previousBackgroundColor;

        public ConsoleColorManager(ConsoleColor foregroundColor)
            : this(foregroundColor, Console.BackgroundColor)
        {
        }

        public ConsoleColorManager(ConsoleColor foregroundColor, ConsoleColor backgroundColor)
        {
            _previousForegroundColor = Console.ForegroundColor;
            _previousBackgroundColor = Console.BackgroundColor;

            Console.ForegroundColor = foregroundColor;
            Console.BackgroundColor = backgroundColor;
        }

        public void Dispose()
        {
            Console.ForegroundColor = _previousForegroundColor;
            Console.BackgroundColor = _previousBackgroundColor;
        }
    }
}