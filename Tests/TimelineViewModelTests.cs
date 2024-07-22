using WpfTimelineControl;

namespace Tests
{
    [TestFixture]
    public class TimelineViewModelTests
    {
        private TimelineViewModel viewModel;

        [SetUp]
        public void Setup()
        {
            var builder = TimelineBuilderFactory.Create();
            viewModel = builder.BuildViewModel(new TimelineBar("Test", DateTime.Now, DateTime.Now.AddMinutes(4), 0));
        }

        [Test]
        public void DEFAULT_MajorIntervalSeconds()
        {
            Assert.That(viewModel.MajorIntervalSeconds, Is.EqualTo(60));
            Assert.That(viewModel.MajorIntervalString, Is.EqualTo("00:01:00"));
        }

        [Test]
        public void DEFAULT_PixelsPerInterval()
        {
            Assert.That(viewModel.PixelsPerInterval, Is.EqualTo(320));
            Assert.That(viewModel.PixelsPerIntervalDecreaseCommand.CanExecute(null), Is.True);
            Assert.That(viewModel.PixelsPerIntervalIncreaseCommand.CanExecute(null), Is.True);
        }

        [Test]
        public void DEFAULT_ShowControls()
        {
            Assert.That(viewModel.ShowControls, Is.True);
        }

        [Test]
        public void DEFAULT_ShowDateMarkers()
        {
            Assert.That(viewModel.ShowDateMarkers, Is.True);
        }

        [Test]
        public void GIVEN_Title_Null_THEN_ShowTitle_False()
        {
            viewModel.Title = null;
            Assert.That(viewModel.ShowTitle, Is.False);
        }

        [Test]
        public void GIVEN_ShowControls_False_AND_Title_Null_THEN_ShowTopBar_False()
        {
            viewModel.ShowControls = false;
            viewModel.Title = null;
            Assert.That(viewModel.ShowTopBar, Is.False);
        }

        [TestCase(true, null)]
        [TestCase(false, "Test")]
        [TestCase(true, "Test")]
        public void GIVEN_ShowControls_True_OR_Title_NotNull_THEN_ShowTopBar_True(bool showControls, string? title)
        {
            viewModel.ShowControls = showControls;
            viewModel.Title = title;
            Assert.That(viewModel.ShowTopBar, Is.True);
        }

        //
        // MajorIntervalSeconds
        //

        [Test]
        public void GIVEN_MajorIntervalSeconds_WHEN_MajorIntervalSecondsDecreaseCommand_Executed_THEN_MajorIntervalSeconds_Decreased()
        {
            List<int> options = TimelineConstants.GetIntervalOptions().ToList();
            viewModel.MajorIntervalSeconds = options[3];
            viewModel.MajorIntervalSecondsDecreaseCommand.Execute(null);
            Assert.That(viewModel.MajorIntervalSeconds, Is.EqualTo(options[2]));
        }

        [Test]
        public void GIVEN_MajorIntervalSeconds_WHEN_MajorIntervalSecondsIncreaseCommand_Executed_THEN_MajorIntervalSeconds_Increased()
        {
            List<int> options = TimelineConstants.GetIntervalOptions().ToList();
            viewModel.MajorIntervalSeconds = options[3];
            viewModel.MajorIntervalSecondsIncreaseCommand.Execute(null);
            Assert.That(viewModel.MajorIntervalSeconds, Is.EqualTo(options[4]));
        }

        [Test]
        public void GIVEN_MajorIntervalSeconds_AtMinimum_THEN_MajorIntervalSecondsDecreaseCommand_CannotBeExecuted()
        {
            List<int> options = TimelineConstants.GetIntervalOptions().ToList();
            viewModel.MajorIntervalSeconds = options[0];
            Assert.That(viewModel.MajorIntervalSecondsDecreaseCommand.CanExecute(null), Is.False);
        }

        [Test]
        public void GIVEN_MajorIntervalSeconds_AtMinimum_THEN_MajorIntervalSecondsIncreaseCommand_CanBeExecuted()
        {
            List<int> options = TimelineConstants.GetIntervalOptions().ToList();
            viewModel.MajorIntervalSeconds = options[0];
            Assert.That(viewModel.MajorIntervalSecondsIncreaseCommand.CanExecute(null), Is.True);
        }

        [Test]
        public void GIVEN_MajorIntervalSeconds_AtMaximum_THEN_MajorIntervalSecondsDecreaseCommand_CanBeExecuted()
        {
            List<int> options = TimelineConstants.GetIntervalOptions().ToList();
            viewModel.MajorIntervalSeconds = options[6];
            Assert.That(viewModel.MajorIntervalSecondsDecreaseCommand.CanExecute(null), Is.True);
        }

        [Test]
        public void GIVEN_MajorIntervalSeconds_AtMaximum_THEN_MajorIntervalSecondsIncreaseCommand_CannotBeExecuted()
        {
            List<int> options = TimelineConstants.GetIntervalOptions().ToList();
            viewModel.MajorIntervalSeconds = options[6];
            Assert.That(viewModel.MajorIntervalSecondsIncreaseCommand.CanExecute(null), Is.False);
        }

        //
        // PixelsPerInterval
        //

        [Test]
        public void GIVEN_PixelsPerInterval_WHEN_PixelsPerIntervalDecreaseCommand_Executed_THEN_PixelsPerInterval_DecreasedBy_10()
        {
            viewModel.PixelsPerInterval = 250;
            viewModel.PixelsPerIntervalDecreaseCommand.Execute(null);
            Assert.That(viewModel.PixelsPerInterval, Is.EqualTo(240));
        }

        [Test]
        public void GIVEN_PixelsPerInterval_WHEN_PixelsPerIntervalIncreaseCommand_Executed_THEN_PixelsPerInterval_IncreasedBy_10()
        {
            viewModel.PixelsPerInterval = 770;
            viewModel.PixelsPerIntervalIncreaseCommand.Execute(null);
            Assert.That(viewModel.PixelsPerInterval, Is.EqualTo(780));
        }

        [Test]
        public void GIVEN_PixelsPerInterval_AtMinimum_THEN_PixelsPerIntervalDecreaseCommand_CannotBeExecuted()
        {
            viewModel.PixelsPerInterval = 40;
            Assert.That(viewModel.PixelsPerIntervalDecreaseCommand.CanExecute(null), Is.False);
        }

        [Test]
        public void GIVEN_PixelsPerInterval_AtMinimum_THEN_PixelsPerIntervalIncreaseCommand_CanBeExecuted()
        {
            viewModel.PixelsPerInterval = 40;
            Assert.That(viewModel.PixelsPerIntervalIncreaseCommand.CanExecute(null), Is.True);
        }

        [Test]
        public void GIVEN_PixelsPerInterval_AtMaximum_THEN_PixelsPerIntervalDecreaseCommand_CanBeExecuted()
        {
            viewModel.PixelsPerInterval = 1000;
            Assert.That(viewModel.PixelsPerIntervalDecreaseCommand.CanExecute(null), Is.True);
        }

        [Test]
        public void GIVEN_PixelsPerInterval_AtMaximum_THEN_PixelsPerIntervalIncreaseCommand_CannotBeExecuted()
        {
            viewModel.PixelsPerInterval = 1000;
            Assert.That(viewModel.PixelsPerIntervalIncreaseCommand.CanExecute(null), Is.False);
        }
    }
}