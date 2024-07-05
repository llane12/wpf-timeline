using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace WpfTimelineControl
{
    public class TimelineViewModel : INotifyPropertyChanged
    {
        private readonly ITimelineBuilder timelineBuilder;

        private string title;
        private bool showControls = true;
        private DateTime start = DateTime.MinValue;
        private DateTime end = DateTime.MinValue;
        private bool showDateMarkers = true;
        private int pixelsPerInterval = 320;
        private int majorIntervalSeconds = 60;

        public TimelineViewModel(ITimelineBuilder timelineBuilder)
        {
            this.timelineBuilder = timelineBuilder;

            MajorIntervalSecondsDecreaseCommand = new MyCommand(MajorIntervalSecondsDecrease, MajorIntervalSecondsCanDecrease);
            MajorIntervalSecondsIncreaseCommand = new MyCommand(MajorIntervalSecondsIncrease, MajorIntervalSecondsCanIncrease);

            PixelsPerIntervalDecreaseCommand = new MyCommand(() => PixelsPerInterval -= 10, () => PixelsPerInterval > 40);
            PixelsPerIntervalIncreaseCommand = new MyCommand(() => PixelsPerInterval += 10, () => PixelsPerInterval < 1000);
        }

        public ObservableCollection<TimelineIntervalMarker> DateMarkers { get; } = new ObservableCollection<TimelineIntervalMarker>();
        public ObservableCollection<TimelineIntervalMarker> IntervalMarkers { get; } = new ObservableCollection<TimelineIntervalMarker>();
        public ObservableCollection<TimelineElement> Entries { get; } = new ObservableCollection<TimelineElement>();

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
                timelineBuilder.RebuildIntervalMarkers(this);
                timelineBuilder.RebuildDateMarkers(this);

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

            List<int> options = TimelineConstants.MajorIntervalOptions.ToList();

            int currentIndex = options.IndexOf(majorIntervalSeconds);

            MajorIntervalSeconds = options[currentIndex - 1];

            PixelsPerInterval = pixelsPerInterval;
        }

        private void MajorIntervalSecondsIncrease()
        {
            if (!MajorIntervalSecondsCanIncrease()) { return; }

            List<int> options = TimelineConstants.MajorIntervalOptions.ToList();

            int currentIndex = options.IndexOf(majorIntervalSeconds);

            MajorIntervalSeconds = options[currentIndex + 1];

            PixelsPerInterval = pixelsPerInterval;
        }

        private bool MajorIntervalSecondsCanDecrease()
        {
            List<int> options = TimelineConstants.MajorIntervalOptions.ToList();

            return options.IndexOf(majorIntervalSeconds) > 0;
        }

        private bool MajorIntervalSecondsCanIncrease()
        {
            List<int> options = TimelineConstants.MajorIntervalOptions.ToList();

            int currentIndex = options.IndexOf(majorIntervalSeconds);

            return currentIndex < (options.Count - 1);
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }

    public class MyCommand : ICommand
    {
        private readonly Action action;
        private readonly Func<bool> canExecute;

        public MyCommand(Action action, Func<bool> canExecute = null)
        {
            this.action = action;
            this.canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return canExecute?.Invoke() ?? true;
        }

        public void Execute(object parameter)
        {
            action?.Invoke();
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
