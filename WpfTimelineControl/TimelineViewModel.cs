using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace WpfTimelineControl
{
    public class TimelineViewModel : ViewModelBase
    {
        private string title;
        private bool showControls = true;
        private DateTime start = DateTime.MinValue;
        private DateTime end = DateTime.MinValue;
        private bool showDateMarkers = true;
        private int pixelsPerInterval = 320;
        private int majorIntervalSeconds = 60;

        public TimelineViewModel()
        {
            MajorIntervalSecondsDecreaseCommand = new MyCommand(MajorIntervalSecondsDecrease, MajorIntervalSecondsCanDecrease);
            MajorIntervalSecondsIncreaseCommand = new MyCommand(MajorIntervalSecondsIncrease, MajorIntervalSecondsCanIncrease);

            PixelsPerIntervalDecreaseCommand = new MyCommand(() => PixelsPerInterval -= 10, () => PixelsPerInterval > 40);
            PixelsPerIntervalIncreaseCommand = new MyCommand(() => PixelsPerInterval += 10, () => PixelsPerInterval < 1000);
        }

        public ObservableCollection<TimelineIntervalMarker> DateMarkers { get; } = new ObservableCollection<TimelineIntervalMarker>();
        public ObservableCollection<TimelineIntervalMarker> IntervalMarkers { get; } = new ObservableCollection<TimelineIntervalMarker>();
        public ObservableCollection<TimelineEntry> Entries { get; } = new ObservableCollection<TimelineEntry>();

        public string Title
        {
            get => title;
            set { title = value; NotifyPropertyChanged(); NotifyPropertyChanged(nameof(ShowTitle)); NotifyPropertyChanged(nameof(ShowTopBar)); }
        }

        public bool ShowTopBar => ShowTitle || ShowControls;
        public bool ShowTitle => !string.IsNullOrWhiteSpace(title);
        public bool ShowControls
        {
            get => showControls;
            set { showControls = value; NotifyPropertyChanged(); NotifyPropertyChanged(nameof(ShowTopBar)); }
        }

        public DateTime Start
        {
            get => start;
            set { start = value; NotifyPropertyChanged(); }
        }

        public DateTime End
        {
            get => end;
            set { end = value; NotifyPropertyChanged(); }
        }

        public bool ShowDateMarkers
        {
            get => showDateMarkers;
            set { showDateMarkers = value; NotifyPropertyChanged(); }
        }

        public MyCommand MajorIntervalSecondsDecreaseCommand { get; }
        public MyCommand MajorIntervalSecondsIncreaseCommand { get; }

        public int MajorIntervalSeconds
        {
            get => majorIntervalSeconds;
            set
            {
                majorIntervalSeconds = value;
                TimelineBuilder.RebuildMarkers(this);

                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(Start));
                NotifyPropertyChanged(nameof(MajorIntervalString));
                MajorIntervalSecondsDecreaseCommand.RaiseCanExecuteChanged();
                MajorIntervalSecondsIncreaseCommand.RaiseCanExecuteChanged();
            }
        }

        public string MajorIntervalString => TimeSpan.FromSeconds(MajorIntervalSeconds).ToString();

        public MyCommand PixelsPerIntervalDecreaseCommand { get; }
        public MyCommand PixelsPerIntervalIncreaseCommand { get; }

        public int PixelsPerInterval
        {
            get => pixelsPerInterval;
            set
            {
                pixelsPerInterval = value;
                NotifyPropertyChanged();
                PixelsPerIntervalDecreaseCommand.RaiseCanExecuteChanged();
                PixelsPerIntervalIncreaseCommand.RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Set the value without raising <see cref="PropertyChanged"/>
        /// </summary>
        internal void SetStart(DateTime value)
        {
            start = value;
        }

        /// <summary>
        /// Set the value without raising <see cref="PropertyChanged"/>
        /// </summary>
        internal void SetMajorIntervalSeconds(int value)
        {
            majorIntervalSeconds = value;
        }

        private void MajorIntervalSecondsDecrease()
        {
            if (!MajorIntervalSecondsCanDecrease()) { return; }

            List<int> options = TimelineConstants.GetIntervalOptions().ToList();

            int currentIndex = options.IndexOf(majorIntervalSeconds);

            MajorIntervalSeconds = options[currentIndex - 1];

            PixelsPerInterval = pixelsPerInterval;
        }

        private void MajorIntervalSecondsIncrease()
        {
            if (!MajorIntervalSecondsCanIncrease()) { return; }

            List<int> options = TimelineConstants.GetIntervalOptions().ToList();

            int currentIndex = options.IndexOf(majorIntervalSeconds);

            MajorIntervalSeconds = options[currentIndex + 1];

            PixelsPerInterval = pixelsPerInterval;
        }

        private bool MajorIntervalSecondsCanDecrease()
        {
            List<int> options = TimelineConstants.GetIntervalOptions().ToList();

            return options.IndexOf(majorIntervalSeconds) > 0;
        }

        private bool MajorIntervalSecondsCanIncrease()
        {
            List<int> options = TimelineConstants.GetIntervalOptions().ToList();

            int currentIndex = options.IndexOf(majorIntervalSeconds);

            return currentIndex < (options.Count - 1);
        }
    }
}
