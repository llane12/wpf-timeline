using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfTimelineControl
{
    public partial class Timeline
    {
        private static readonly Brush[] defaultBrushes = new SolidColorBrush[]
        {
            Brushes.Red, Brushes.Green, Brushes.Blue, Brushes.Yellow, Brushes.Magenta,
        };

        public Timeline()
        {
            InitializeComponent();
        }

        public Brush[] TimelineEntryBrushes
        {
            get => (Brush[])GetValue(TimelineEntryBrushesProperty);
            set => SetValue(TimelineEntryBrushesProperty, value);
        }

        public bool ColoredEntryNameLabels
        {
            get { return (bool)GetValue(ColoredEntryNameLabelsProperty); }
            set { SetValue(ColoredEntryNameLabelsProperty, value); }
        }

        /// <summary>
        /// Support for translation
        /// </summary>
        public string EventLabel
        {
            get { return (string)GetValue(EventLabelProperty); }
            set { SetValue(EventLabelProperty, value); }
        }

        /// <summary>
        /// Support for translation
        /// </summary>
        public string StartLabel
        {
            get { return (string)GetValue(StartLabelProperty); }
            set { SetValue(StartLabelProperty, value); }
        }

        /// <summary>
        /// Support for translation
        /// </summary>
        public string EndLabel
        {
            get { return (string)GetValue(EndLabelProperty); }
            set { SetValue(EndLabelProperty, value); }
        }

        public int LabelsExceedBarsSettingColorIndex
        {
            get { return (int)GetValue(LabelsExceedBarsSettingColorIndexProperty); }
            set { SetValue(LabelsExceedBarsSettingColorIndexProperty, value); }
        }

        public bool LabelsExceedBars
        {
            get { return (bool)GetValue(LabelsExceedBarsProperty); }
            set { SetValue(LabelsExceedBarsProperty, value); }
        }

        public static readonly DependencyProperty TimelineEntryBrushesProperty = DependencyProperty.Register(
                nameof(TimelineEntryBrushes),
                typeof(SolidColorBrush[]),
                typeof(Timeline),
                new PropertyMetadata(defaultBrushes));

        public static readonly DependencyProperty ColoredEntryNameLabelsProperty = DependencyProperty.Register(
                nameof(ColoredEntryNameLabels),
                typeof(bool),
                typeof(Timeline),
                new PropertyMetadata(true));

        public static readonly DependencyProperty EventLabelProperty = DependencyProperty.Register(
                nameof(EventLabel),
                typeof(string),
                typeof(Timeline),
                new PropertyMetadata("Event"));

        public static readonly DependencyProperty StartLabelProperty = DependencyProperty.Register(
                nameof(StartLabel),
                typeof(string),
                typeof(Timeline),
                new PropertyMetadata("Start"));

        public static readonly DependencyProperty EndLabelProperty = DependencyProperty.Register(
                nameof(EndLabel),
                typeof(string),
                typeof(Timeline),
                new PropertyMetadata("End"));

        protected static readonly DependencyProperty LabelsExceedBarsSettingColorIndexProperty = DependencyProperty.Register(
                nameof(LabelsExceedBarsSettingColorIndex),
                typeof(int),
                typeof(Timeline),
                new PropertyMetadata(0));

        protected static readonly DependencyProperty LabelsExceedBarsProperty = DependencyProperty.Register(
                nameof(LabelsExceedBars),
                typeof(bool),
                typeof(Timeline),
                new PropertyMetadata(true));

        /// <summary>
        /// This is only here as a way to pass a value from the parent timeline control to the ToolTips,
        /// which are not part of the same visual tree so cannot access the dependency properties.
        /// </summary>
        private void ToolTip_Opened(object sender, RoutedEventArgs e)
        {
            var toolTip = sender as ToolTip;
            var timelineEntry = toolTip.DataContext as TimelineEntry;

            timelineEntry.StartLabel = StartLabel;
            timelineEntry.EndLabel = EndLabel;
        }
    }
}
