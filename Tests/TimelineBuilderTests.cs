using WpfTimelineControl;

namespace Tests
{
    [TestFixture]
    public class TimelineBuilderTests
    {
        private List<TimelineEntry> threeSortedMixedEntries;
        private List<TimelineEntry> fourUnsortedEntries;

        private ITimelineBuilder builder;

        [SetUp]
        public void Setup()
        {
            TimelineConstants.ResetScalingFactor();
            TimelineConstants.ResetIntervalOptions();

            builder = TimelineBuilderFactory.Create();

            DateTime today = DateTime.Today;
            DateTime start = new DateTime(today.Year, today.Month, today.Day, 13, today.Minute, 0);

            // Duration = 00:14:00 = 840secs
            threeSortedMixedEntries = new List<TimelineEntry>
            {
                new TimelineBar("Event 1", start.AddMinutes(1), start.AddMinutes(2), 0),
                new TimelinePoint("Event 2", start.AddMinutes(5), start.AddMinutes(7), 1),
                new TimelineBar("Event 3", start.AddMinutes(12), start.AddMinutes(14), 2)
            };

            // Duration = 00:07:00 = 420secs
            fourUnsortedEntries = new List<TimelineEntry>
            {
                new TimelineBar("Event A", start.AddMinutes(2), start.AddMinutes(4), 0),
                new TimelineBar("Event B", start.AddMinutes(1), start.AddMinutes(5), 1),
                new TimelineBar("Event D", start.AddMinutes(5), start.AddMinutes(7), 3),
                new TimelineBar("Event C", start.AddMinutes(3), start.AddMinutes(1), 2),
            };
        }

        [Test]
        public void GIVEN_TimelineEntries_Null_WHEN_BuildViewModel_THEN_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => builder.BuildViewModel(null));
        }

        [Test]
        public void GIVEN_TimelineEntries_Empty_WHEN_BuildViewModel_THEN_ArgumentException()
        {
            Assert.Throws<ArgumentException>(() => builder.BuildViewModel());
            Assert.Throws<ArgumentException>(() => builder.BuildViewModel(new TimelineEntry[] { }));
        }

        [Test]
        public void GIVEN_TimelineEntries_ThreeSortedMixed_WHEN_BuildViewModel_THEN_ThreeEntries()
        {
            DateTime start = DateTime.Now;
            var viewModel = builder.BuildViewModel(threeSortedMixedEntries.ToArray());
            Assert.That(viewModel.Entries, Has.Count.EqualTo(3));
        }

        [Test]
        public void GIVEN_TimelineEntries_FourUnsorted_WHEN_BuildViewModel_THEN_EntriesAreSorted()
        {
            DateTime start = DateTime.Now;
            var viewModel = builder.BuildViewModel(fourUnsortedEntries.ToArray());
            Assert.That(viewModel.Entries[0], Is.EqualTo(fourUnsortedEntries[1]));
            Assert.That(viewModel.Entries[1], Is.EqualTo(fourUnsortedEntries[0]));
            Assert.That(viewModel.Entries[3], Is.EqualTo(fourUnsortedEntries[2]));
            Assert.That(viewModel.Entries[2], Is.EqualTo(fourUnsortedEntries[3]));
        }

        [Test]
        public void GIVEN_TimelineConstants_AND_TimelineEntries_ThreeSortedMixed_WHEN_BuildViewModel_THEN_ClosestMajorIntervalSelected()
        {
            TimelineConstants.SetIntervalOptions(new int[] { 30, 60, 300, 600, 1800 });
            TimelineConstants.SetScalingFactor(2.0);
            var viewModel = builder.BuildViewModel(threeSortedMixedEntries.ToArray());
            // Target = 840secs / 2.0 = 420, 300 is closest
            Assert.That(viewModel.MajorIntervalSeconds, Is.EqualTo(300));
        }

        [Test]
        public void GIVEN_TimelineConstants_AND_TimelineEntries_ThreeSortedMixed_WHEN_BuildViewModel_THEN_IntervalMarkersCreated()
        {
            TimelineConstants.SetIntervalOptions(new int[] { 30, 60, 300, 600, 1800 });
            TimelineConstants.SetScalingFactor(2.0);
            var viewModel = builder.BuildViewModel(threeSortedMixedEntries.ToArray());
            // Need to cover 14minutes with intervals of 5 minutes = 3 markers
            Assert.That(viewModel.IntervalMarkers, Has.Count.EqualTo(3));
        }

        [Test]
        public void GIVEN_TimelineConstants_AND_TimelineEntries_ThreeSortedMixed_WHEN_BuildViewModel_THEN_DateMarkersCreated()
        {
            var viewModel = builder.BuildViewModel(threeSortedMixedEntries.ToArray());
            // All events occur on the same day, so 1 date marker needed
            Assert.That(viewModel.DateMarkers, Has.Count.EqualTo(1));
        }
    }
}
