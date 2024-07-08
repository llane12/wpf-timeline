using System;
using System.Collections.Generic;
using System.Linq;

namespace WpfTimelineControl
{
    public interface ITimelineBuilder
    {
        TimelineViewModel BuildViewModel(params TimelineElement[] timelineEntries);
    }

    public static class TimelineBuilderFactory
    {
        public static ITimelineBuilder Create()
        {
            return new TimelineBuilder();
        }
    }

    internal class TimelineBuilder : ITimelineBuilder
    {
        public TimelineViewModel BuildViewModel(params TimelineElement[] timelineEntries)
        {
            if (timelineEntries == null) throw new ArgumentNullException(nameof(timelineEntries));

            TimelineViewModel viewModel = new TimelineViewModel();

            foreach (var entry in timelineEntries.OrderBy(t => t.Start))
            {
                viewModel.Elements.Add(entry);
            }

            if (viewModel.Elements.Count == 0) return viewModel;

            RecalculateMajorInterval(viewModel);
            RebuildIntervalMarkers(viewModel);
            RebuildDateMarkers(viewModel);

            return viewModel;
        }

        /// <summary>
        /// <para>Find the best major interval value based on the total duration of the events
        /// (from the beggining of the first to the end of the last).<br/>
        /// Assumes the collection of <see cref="TimelineElement"/> used to build the <see cref="TimelineViewModel"/> has been sorted.</para>
        /// </summary>
        private static void RecalculateMajorInterval(TimelineViewModel viewModel)
        {
            DateTime earliestStart = viewModel.Elements.First().Start; // assume entries are sorted
            DateTime latestEnd = viewModel.Elements.Max(e => e.End);

            TimeSpan durationOfAllEntries = latestEnd - earliestStart;

            if (durationOfAllEntries <= TimeSpan.Zero)
            {
                viewModel.MajorIntervalSeconds = 60;
                return;
            }

            var majorIntervalOptions = new List<int>(TimelineConstants.MajorIntervalOptions);

            double targetNumber = durationOfAllEntries.TotalSeconds / TimelineConstants.TimelineScalingFactor;
            int closestOption = majorIntervalOptions.OrderBy(v => Math.Abs(v - targetNumber)).First();
            viewModel.SetMajorIntervalSeconds(closestOption);
        }

        /// <summary>
        /// Clear and rebuild the Timeline interval markers, these are the time markers along the top and vertical bars.
        /// </summary>
        internal static void RebuildIntervalMarkers(TimelineViewModel viewModel)
        {
            viewModel.IntervalMarkers.Clear();

            DateTime start = viewModel.Elements.First().Start; // assume entries are sorted

            DateTime position = start.Date; // start at the beginning of the day

            TimeSpan interval = TimeSpan.FromSeconds(viewModel.MajorIntervalSeconds);

            // find the last interval before the first entry
            while (position + interval <= start)
            {
                position += interval;
            }
            addIntervalMarker(position, interval, true);

            viewModel.SetStart(position);

            // keep adding until the last interval is just before the latest entry
            DateTime latestEnd = viewModel.Elements.Max(e => e.End);
            while (position + interval <= latestEnd)
            {
                position += interval;
                addIntervalMarker(position, interval, false);
            }
            viewModel.End = position + interval;

            void addIntervalMarker(DateTime p, TimeSpan i, bool first)
            {
                bool showSeconds = i.TotalSeconds < 60;
                viewModel.IntervalMarkers.Add(new TimelineIntervalMarker(p, i, showSeconds ? p.ToLongTimeString() : p.ToShortTimeString(), first));
            }
        }

        /// <summary>
        /// <para>Clear and rebuild the date markers along the top of the Timeline.<br/>
        /// Make sure to call <see cref="RebuildIntervalMarkers"/> first, so the <see cref="TimelineViewModel.Start"/> value is re-calculated.</para>
        /// </summary>
        internal static void RebuildDateMarkers(TimelineViewModel viewModel)
        {
            viewModel.DateMarkers.Clear();

            if (viewModel.Start.Date == viewModel.End.Date)
            {
                viewModel.DateMarkers.Add(new TimelineIntervalMarker(viewModel.Start, viewModel.End, viewModel.Start.Date.ToShortDateString(), true));
                return;
            }

            DateTime start = viewModel.Start;
            DateTime end = start.Date.AddDays(1);
            viewModel.DateMarkers.Add(new TimelineIntervalMarker(start, end, start.Date.ToShortDateString(), true));

            while (end.Date < viewModel.End.Date)
            {
                start = end;
                end = end.AddDays(1);
                viewModel.DateMarkers.Add(new TimelineIntervalMarker(start, end, start.Date.ToShortDateString(), false));
            }

            start = end;
            viewModel.DateMarkers.Add(new TimelineIntervalMarker(start, viewModel.End, start.Date.ToShortDateString(), false));
        }
    }
}
