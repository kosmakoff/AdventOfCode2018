using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Day03.Models;
using static Common.Utils;

namespace Day03
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintHeader("Day 03");

            var input = File.ReadAllLines("Input.txt")
                .Select(ParseClaim)
                .ToList();

            var answer1 = CalculateAnswer1(input);
            PrintAnswer("Answer 1", answer1);

            var answer2 = CalculateAnswer2(input);
            PrintAnswer("Answer 2", answer2);
        }

        // extremely inefficient way to calculate it, using 2D grid could be faster
        private static int CalculateAnswer1(IReadOnlyCollection<Claim> input)
        {
            return Enumerable.Range(0, 1000)
                .SelectMany(x => Enumerable.Range(0, 1000).Select(y => (X: x, Y: y)))
                .AsParallel()
                .Select(coords => input.Where(i => i.ContainsPoint(coords.X, coords.Y)).Take(2).ToList())
                .Count(list => list.Count == 2);
        }

        private static Claim CalculateAnswer2(IReadOnlyCollection<Claim> input)
        {
            return input
                .Single(claim =>
                {
                    var others = input.Except(new[] {claim}).ToList();
                    return !others.Any(otherClaim => Claim.Intersects(claim, otherClaim));
                });
        }

        private static readonly Regex ClaimRegex = new Regex(@"^#(?<id>\d+) @ (?<x>\d+),(?<y>\d+): (?<width>\d+)x(?<height>\d+)$", RegexOptions.Compiled);

        private static Claim ParseClaim(string input)
        {
            var match = ClaimRegex.Match(input);
            if (!match.Success)
                throw new ArgumentException($"Incorrect claim format in {input}", nameof(input));

            var id = int.Parse(match.Groups["id"].Value);
            var x = int.Parse(match.Groups["x"].Value);
            var y = int.Parse(match.Groups["y"].Value);
            var width = int.Parse(match.Groups["width"].Value);
            var height = int.Parse(match.Groups["height"].Value);

            return new Claim(id, new Offset(x, y), new Size(width, height));
        }
    }
}
