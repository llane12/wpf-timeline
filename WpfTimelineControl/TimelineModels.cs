using System;

namespace WpfTimelineControl
{
    public abstract class TimelineElement
    {
        public TimelineElement(string name, DateTime start, TimeSpan duration)
            : this(name, start, start + duration, duration)
        {
        }

        public TimelineElement(string name, DateTime start, DateTime end)
            : this(name, start, end, end - start)
        {
        }

        private TimelineElement(string name, DateTime start, DateTime end, TimeSpan duration)
        {
            Name = name;
            Start = start;
            End = end;
            Duration = duration;
        }

        public string Name { get; }
        public DateTime Start { get; }
        public DateTime End { get; }
        public TimeSpan Duration { get; }
        public bool HasDuration => Duration != null && Duration > TimeSpan.Zero;
    }

    public enum TimelineEntryType
    {
        Bar,
        Point,
    }

    public abstract class TimelineEntry : TimelineElement
    {
        public TimelineEntry(string name, DateTime start, TimeSpan duration, TimelineEntryType entryType)
            : base(name, start, duration)
        {
            EntryType = entryType;
        }

        public TimelineEntry(string name, DateTime start, DateTime end, TimelineEntryType entryType)
            : base(name, start, end)
        {
            EntryType = entryType;
        }

        public TimelineEntryType EntryType { get; }
    }

    public class TimelineBar : TimelineEntry
    {
        public TimelineBar(string name, DateTime start, TimeSpan duration, int colorIndex)
            : base(name, start, duration, TimelineEntryType.Bar)
        {
            ColorIndex = colorIndex;
        }

        public TimelineBar(string name, DateTime start, DateTime end, int colorIndex)
            : base(name, start, end, TimelineEntryType.Bar)
        {
            ColorIndex = colorIndex;
        }

        public int ColorIndex { get; }
    }

    /// <summary>
    /// Sometimes events are short (or the timeframe in which they took place is very long) so it is better to represent them
    /// as a point in time rather than a bar with a duration.
    /// </summary>
    public class TimelinePoint : TimelineEntry
    {
        public TimelinePoint(string name, DateTime point, int colorIndex)
            : base(name, point, point, TimelineEntryType.Point)
        {
            ColorIndex = colorIndex;
        }

        public TimelinePoint(string name, DateTime start, TimeSpan duration, int colorIndex)
            : base(name, start, duration, TimelineEntryType.Point)
        {
            ColorIndex = colorIndex;
        }

        public TimelinePoint(string name, DateTime start, DateTime end, int colorIndex)
            : base(name, start, end, TimelineEntryType.Point)
        {
            ColorIndex = colorIndex;
        }

        public int ColorIndex { get; }
    }
}
