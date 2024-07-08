using System;
using System.Collections.Generic;
using WpfTimelineControl;

namespace TestApp
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            var yesterday = DateTime.Today.AddDays(-1);
            var year = yesterday.Year;
            var month = yesterday.Month;
            var day = yesterday.Day;

            List<TimelineElement> entries = new List<TimelineElement>
            {
                new TimelineBar("Event 1", new DateTime(year, month, day, 8, 6, 14), TimeSpan.FromSeconds(128), 0),
                new TimelineBar("Event 2", new DateTime(year, month, day, 8, 6, 20), TimeSpan.FromSeconds(344), 1),
                new TimelinePoint("Event 3", new DateTime(year, month, day, 8, 27, 1), TimeSpan.FromSeconds(31), 2),
                new TimelineBar("Event 4", new DateTime(year, month, day, 9, 23, 33), TimeSpan.FromSeconds(1001), 3),
                new TimelineBar("Event 5", new DateTime(year, month, day, 9, 59, 22), TimeSpan.FromSeconds(550), 4),
                new TimelineBar("Event 6", new DateTime(year, month, day, 10, 16, 19), TimeSpan.FromSeconds(197), 5),
                new TimelineBar("Event 7", new DateTime(year, month, day, 10, 30, 47), TimeSpan.FromSeconds(262), 6),
                new TimelinePoint("Event 8", new DateTime(year, month, day, 11, 44, 53), TimeSpan.FromSeconds(21), 0),
                new TimelinePoint("Event 9", new DateTime(year, month, day, 12, 00, 14), TimeSpan.FromSeconds(15), 1),
                new TimelineBar("Event 10", new DateTime(year, month, day, 12, 58, 9), TimeSpan.FromSeconds(212), 2),
                new TimelineBar("Event 11", new DateTime(year, month, day, 13, 49, 44), TimeSpan.FromSeconds(303), 3),
                new TimelineBar("Event 12", new DateTime(year, month, day, 14, 37, 32), TimeSpan.FromSeconds(840), 4)
            };

            var timelineBuilder = TimelineBuilderFactory.Create();

            var viewModel = timelineBuilder.BuildViewModel(entries.ToArray());
            viewModel.PixelsPerInterval = 160;

            myTimeline.DataContext = viewModel;
        }
    }
}
