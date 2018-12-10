using System.Collections.Generic;
using System.IO;
using System.Linq;
using MoreLinq.Experimental;
using MoreLinq.Extensions;
using static Common.Utils;

namespace Day01
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintHeader("Day 01");

            var input = File.ReadAllLines("Input.txt");
            var answer1 = input
                .Select(int.Parse)
                .Sum();

            var answer2 = CalculateAnswer2(input.Select(int.Parse));

            PrintAnswer("Answer 1", answer1);
            PrintAnswer("Answer 2", answer2);
        }

        private static int CalculateAnswer2(IEnumerable<int> source)
        {
            var hash = new HashSet<int> {0};
            var current = 0;

            foreach (var i in source.Memoize().Repeat())
            {
                current += i;

                if (!hash.Add(current))
                    return current;
            }

            return 0;
        }
    }
}
