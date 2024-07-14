using WpfTimelineControl;
using System;

namespace TestApp
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            var timelineData = new TimelineData();

            timelineData.AddEntries(
                new TimelineEntry(new DateTime(2024, 1, 10, 8, 6, 14), TimeSpan.FromSeconds(28), "Event 1"),
                new TimelineEntry(new DateTime(2024, 1, 10, 8, 6, 20), TimeSpan.FromSeconds(344), "Event 2"),
                new TimelineEntry(new DateTime(2024, 1, 10, 8, 27, 1), TimeSpan.FromSeconds(31), "Event 3"),
                new TimelineEntry(new DateTime(2024, 1, 10, 9, 23, 33), TimeSpan.FromSeconds(1001), "Event 4"),
                new TimelineEntry(new DateTime(2024, 1, 10, 9, 59, 22), TimeSpan.FromSeconds(550), "Event 5"),
                new TimelineEntry(new DateTime(2024, 1, 10, 10, 16, 19), TimeSpan.FromSeconds(97), "Event 6"),
                new TimelineEntry(new DateTime(2024, 1, 10, 10, 30, 47), TimeSpan.FromSeconds(262), "Event 7"),
                new TimelineEntry(new DateTime(2024, 1, 10, 11, 44, 53), TimeSpan.FromSeconds(61), "Event 8"),
                new TimelineEntry(new DateTime(2024, 1, 10, 12, 00, 14), TimeSpan.FromSeconds(50), "Event 9"),
                new TimelineEntry(new DateTime(2024, 1, 10, 12, 17, 9), TimeSpan.FromSeconds(212), "Event 10"));

            myTimeline.TimelineData = timelineData;
        }
    }
}
