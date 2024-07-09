using System;

namespace WpfTimelineControl
{
    /// <summary>
    /// Base class for the timeline entries (bars and points) and interval markers
    /// </summary>
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
    /// Sometimes events are short and look very small on a timeline where their width represents the duration,
    /// so it is better to represent them as a point in time with a fixed-width shape.
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

    /// <summary>
    /// Used to represent the intervals and vertical bars across the timeline as well as the Days above the timeline 
    /// </summary>
    public class TimelineIntervalMarker : TimelineElement
    {
        public TimelineIntervalMarker(DateTime start, TimeSpan duration, string name, bool first)
            : base(name, start, duration)
        {
            First = first;
        }

        public TimelineIntervalMarker(DateTime start, DateTime end, string name, bool first)
            : base(name, start, end)
        {
            First = first;
        }

        public bool First { get; private set; }
    }
}
