using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MoreLinq;
using static Common.Utils;

namespace Day02
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            PrintHeader("Day 02");

            var input = File.ReadAllLines("Input.txt");

            var answer1 = CalculateAnswer1(input);
            PrintAnswer("Answer 1", answer1);

            var answer2 = CalculateAnswer2(input);
            PrintAnswer("Answer 2", answer2);
        }

        private static int CalculateAnswer1(string[] input)
        {
            var preResult = input
                .Select(ToDictWithCounts)
                .Select(d => (HasTwos: d.Values.Contains(2), HasThrees: d.Values.Contains(3)))
                .Aggregate((Twos: 0, Threes: 0),
                    (acc, i) => (
                        acc.Twos + (i.HasTwos ? 1 : 0),
                        acc.Threes + (i.HasThrees ? 1 : 0)));

            return preResult.Twos * preResult.Threes;
        }

        private static string CalculateAnswer2(string[] input)
        {
            return input.Cartesian(input, (first, second) => (First: first, Second: second))
                .Where(a => a.First != a.Second)
                .Where(a => IsDiffOneCharOnly(a.First, a.Second))
                .Select(a => CalculateCommonChars(a.First, a.Second))
                .First();
        }

        private static string CalculateCommonChars(string argItem1, string argItem2)
        {
            return argItem1
                .Zip(argItem2, (c1, c2) => (C1: c1, C2: c2))
                .Where(a => a.C1 == a.C2)
                .Select(a => a.C1)
                .Aggregate(new StringBuilder(argItem1.Length), (sb, c) => sb.Append(c), sb => sb.ToString());
        }

        private static IDictionary<char, int> ToDictWithCounts(string arg)
        {
            return arg.GroupBy(c => c).ToDictionary(g => g.Key, g => g.Count());
        }

        private static bool IsDiffOneCharOnly(string argItem1, string argItem2)
        {
            var preResult = argItem1
                .Zip(argItem2, (c1, c2) => (C1: c1, C2: c2))
                .Select(a => Math.Abs(a.C1 - a.C2))
                .Where(i => i > 0)
                .OrderByDescending(i => i)
                .ToList();

            return preResult.Count == 1;
        }
    }
}