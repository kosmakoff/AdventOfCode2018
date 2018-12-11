using System;

namespace Day04
{
    public class Note
    {
        public DateTimeOffset DateTime { get; }
        public Action Action { get; }
        public int GuardId { get; }

        public Note(DateTimeOffset dateTime, Action action, int guardId = 0)
        {
            DateTime = dateTime;
            Action = action;
            GuardId = guardId;
        }
    }
}
