using System;

namespace Common
{
    public class Utils
    {
        public static void PrintHeader(string header)
        {
            using (new ConsoleColorManager(ConsoleColor.Magenta))
            {
                Console.Write("********** ");
                Console.Write(header);
                Console.WriteLine(" **********");
                Console.WriteLine();
            }
        }

        public static string Prompt(string prompt)
        {
            using (new ConsoleColorManager(ConsoleColor.DarkYellow))
            {
                Console.Write($"{prompt}: ");
            }

            using (new ConsoleColorManager(ConsoleColor.Cyan))
            {
                return Console.ReadLine();
            }
        }

        public static void PrintAnswer(string title, object answer)
        {
            using (new ConsoleColorManager(ConsoleColor.Green))
            {
                Console.WriteLine($"{title}: {answer}");
            }
        }
    }
}