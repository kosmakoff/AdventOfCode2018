using System;
using System.Collections;

namespace Day04
{
    public class GuardSchedule
    {
        public int GuardId { get; }
        public DateTimeOffset Date { get; }
        public BitArray AwakenessChart { get; }

        public GuardSchedule(int guardId, DateTimeOffset date, BitArray awakenessChart)
        {
            GuardId = guardId;
            Date = date;
            AwakenessChart = awakenessChart;
        }
    }
}
