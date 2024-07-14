using System;
using System.Collections.Generic;
using System.Linq;

namespace WpfTimelineControl
{
    /// <summary>
    /// Not meant to be long-lived. If you need to 'reset', just make a new one.
    /// </summary>
    public class TimelineData
    {
        private DateTime latestEnd;

        public List<TimelineEntry> Entries { get; } = new List<TimelineEntry>();
        public List<TimelineIntervalMarker> IntervalMarkers { get; } = new List<TimelineIntervalMarker>();
        public int MajorIntervalMinutes { get; private set; } = 1;
        public DateTime Start { get; private set; } = DateTime.MinValue;
        public DateTime End { get; private set; } = DateTime.MinValue;

        public void AddEntry(TimelineEntry timelineEntry)
        {
            if (timelineEntry == null) throw new ArgumentNullException(nameof(timelineEntry));

            AddEntries(timelineEntry);
        }

        public void AddEntries(params TimelineEntry[] timelineEntries)
        {
            if (timelineEntries == null) throw new ArgumentNullException(nameof(timelineEntries));

            Entries.AddRange(timelineEntries);

            Entries.Sort((x, y) => x.Start.CompareTo(y.Start));

            RecalculateMajorInterval();
            RecalculateIntervalMarkers();
        }

        private void RecalculateMajorInterval()
        {
            DateTime earliestStart = Entries.First().Start; // assume entries are sorted
            latestEnd = Entries.Max(e => e.End);

            TimeSpan durationOfAllEntries = latestEnd - earliestStart;

            if (durationOfAllEntries <= TimeSpan.Zero) { MajorIntervalMinutes = 1; return; }

            var majorIntervalOptions = new List<int>(TimelineConstants.MajorIntervalOptions);

            // algorithm source: https://stackoverflow.com/a/10120978/11646956
            long targetNumber = (long)(durationOfAllEntries.TotalMinutes / TimelineConstants.TimelineScalingFactor);
            MajorIntervalMinutes = majorIntervalOptions.OrderBy(v => Math.Abs(v - targetNumber)).First();
        }

        private void RecalculateIntervalMarkers()
        {
            IntervalMarkers.Clear();

            DateTime start = Entries.First().Start; // assume entries are sorted

            DateTime position = start.Date; // start at the beginning of the day

            TimeSpan interval = TimeSpan.FromMinutes(MajorIntervalMinutes);

            // find the last interval before the first entry
            while (position + interval <= start)
            {
                position += interval;
            }
            IntervalMarkers.Add(new TimelineIntervalMarker(position, interval, position.ToShortTimeString(), true));
            Start = position;

            // keep adding until the last interval is just before the latest entry
            while (position + interval <= latestEnd)
            {
                position += interval;
                IntervalMarkers.Add(new TimelineIntervalMarker(position, interval, position.ToShortTimeString(), false));
            }
            End = position + interval;
        }
    }

    public class TimelineEntry
    {
        public TimelineEntry(DateTime start, TimeSpan duration, string name)
        {
            Start = start;
            Duration = duration;
            Name = name;
        }

        public DateTime Start { get; set; }
        public TimeSpan Duration { get; set; }
        public string Name { get; set; }

        public DateTime End => Start + Duration;
    }

    public class TimelineIntervalMarker : TimelineEntry
    {
        public TimelineIntervalMarker(DateTime start, TimeSpan duration, string name, bool first)
            : base(start, duration, name)
        {
            First = first;
        }

        public bool First { get; private set; }
    }
}
