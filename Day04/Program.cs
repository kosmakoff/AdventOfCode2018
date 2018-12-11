using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using static Common.Utils;

namespace Day04
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintHeader("Day 04");

            var input = File.ReadAllLines("Input.txt")
                .Select(ParseNote)
                .OrderBy(note => note.DateTime)
                .ToList();

            var schedules = ParseSchedules(input).ToList();

            var answer1 = CalculateAnswer1(schedules);
            PrintAnswer("Answer 1", answer1);

            var answer2 = CalculateAnswer2(schedules);
            PrintAnswer("Answer 2", answer2);
        }

        private static int CalculateAnswer1(IEnumerable<GuardSchedule> schedules)
        {
            var mostSleepyGuard = schedules
                .GroupBy(schedule => schedule.GuardId)
                .Select(grp => (GuardId: grp.Key, SleepinessSum: SumSleepiness(grp), TotalMinutesAsleep: SumMinutesAsleep(grp)))
                .OrderByDescending(a => a.TotalMinutesAsleep)
                .First();

            var mostSleepyMinute = mostSleepyGuard.SleepinessSum
                .Select((sleepTimes, minute) => (SleepTimes: sleepTimes, Minute: minute))
                .OrderByDescending(a => a.SleepTimes)
                .First()
                .Minute;

            return mostSleepyGuard.GuardId * mostSleepyMinute;
        }

        private static int CalculateAnswer2(IEnumerable<GuardSchedule> schedules)
        {
            var mostSystematicSleeper = schedules
                .GroupBy(schedule => schedule.GuardId)
                .SelectMany(grp => SumSleepiness(grp).Select((sleepTimes, minute) =>
                    (GuardId: grp.Key, Minute: minute, SleepTimes: sleepTimes)))
                .OrderByDescending(a => a.SleepTimes)
                .Select(a => (GuardId: a.GuardId, MostSleepyMinute: a.Minute))
                .First();


            return mostSystematicSleeper.GuardId * mostSystematicSleeper.MostSleepyMinute;
        }

        private static int SumMinutesAsleep(IEnumerable<GuardSchedule> groupSchedules)
        {
            return groupSchedules.SelectMany(schedule => schedule.AwakenessChart.Cast<bool>()).Count(b => !b);
        }

        private static int[] SumSleepiness(IEnumerable<GuardSchedule> groupSchedules)
        {
            var result = new int[60];

            foreach (var groupSchedule in groupSchedules)
            {
                for (int i = 0; i < 60; i++)
                {
                    if (!groupSchedule.AwakenessChart[i])
                        result[i]++;
                }
            }

            return result;
        }

        private static IEnumerable<GuardSchedule> ParseSchedules(IEnumerable<Note> input)
        {
            var processingQueue = new Stack<Note>(input.Reverse());

            while (processingQueue.Any())
            {
                var guardIdNote = processingQueue.Pop();

                var awakenessChart = new BitArray(60, true);
                int lastSleepStart = -1;

                while (processingQueue.Any() && processingQueue.Peek().Action != Action.ShiftBegin)
                {
                    var nextNote = processingQueue.Pop();

                    switch (nextNote.Action)
                    {
                        case Action.FallAsleep:
                            lastSleepStart = nextNote.DateTime.Minute;
                            break;
                        case Action.WakeUp:
                            var awakeStart = nextNote.DateTime.Minute;
                            for (int minute = lastSleepStart; minute < awakeStart; minute++)
                            {
                                awakenessChart[minute] = false;
                            }

                            lastSleepStart = -1;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }

                if (lastSleepStart >= 0)
                {
                    for (int minute = lastSleepStart; minute < 60; minute++)
                    {
                        awakenessChart[minute] = false;
                    }
                }

                yield return new GuardSchedule(guardIdNote.GuardId, guardIdNote.DateTime.Date, awakenessChart);
            }
        }

        private static Note ParseNote(string input)
        {
            var dateTimePart = input.Substring(1, 16);
            var dateTime = DateTimeOffset.ParseExact(dateTimePart, "yyyy-MM-dd HH:mm",
                CultureInfo.InvariantCulture.DateTimeFormat);

            Action action;
            int guardId = 0;

            switch (input[19])
            {
                case 'G':
                    action = Action.ShiftBegin;
                    var guardIdStringEnd = input.IndexOf(' ', 26);
                    var guardIdString = input.Substring(26, guardIdStringEnd - 26);
                    guardId = int.Parse(guardIdString);
                    break;
                case 'f':
                    action = Action.FallAsleep;
                    break;
                case 'w':
                    action = Action.WakeUp;
                    break;
                default:
                    throw new NotSupportedException($"Do not support input {input}");
            }

            return new Note(dateTime, action, guardId);
        }
    }
}
