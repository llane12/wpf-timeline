using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfTimelineControl
{
    public partial class Timeline
    {
        private static readonly SolidColorBrush[] defaultBrushes = new SolidColorBrush[]
        {
            Brushes.Red, Brushes.Green, Brushes.Blue, Brushes.Yellow, Brushes.Magenta,
        };

        public Timeline()
        {
            InitializeComponent();
        }

        public SolidColorBrush[] TimelineEntryBrushes
        {
            get => (SolidColorBrush[])GetValue(TimelineEntryBrushesProperty);
            set => SetValue(TimelineEntryBrushesProperty, value);
        }

        /// <summary>
        /// Support for translation
        /// </summary>
        public string NameLabel
        {
            get { return (string)GetValue(NameLabelProperty); }
            set { SetValue(NameLabelProperty, value); }
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

        protected bool LabelsExceedBars
        {
            get { return (bool)GetValue(LabelsExceedBarsProperty); }
            set { SetValue(LabelsExceedBarsProperty, value); }
        }

        public static readonly DependencyProperty TimelineEntryBrushesProperty = DependencyProperty.Register(
                "TimelineEntryBrushes",
                typeof(SolidColorBrush[]),
                typeof(Timeline),
                new PropertyMetadata(defaultBrushes));

        public static readonly DependencyProperty NameLabelProperty = DependencyProperty.Register(
                "NameLabel",
                typeof(string),
                typeof(Timeline),
                new PropertyMetadata("Name"));

        public static readonly DependencyProperty StartLabelProperty = DependencyProperty.Register(
            "StartLabel",
            typeof(string),
            typeof(Timeline),
            new PropertyMetadata("Start"));

        public static readonly DependencyProperty EndLabelProperty = DependencyProperty.Register(
            "EndLabel",
            typeof(string),
            typeof(Timeline),
            new PropertyMetadata("End"));

        protected static readonly DependencyProperty LabelsExceedBarsSettingColorIndexProperty = DependencyProperty.Register(
                "LabelsExceedBarsSettingColorIndex",
                typeof(int),
                typeof(Timeline),
                new PropertyMetadata(0));

        protected static readonly DependencyProperty LabelsExceedBarsProperty = DependencyProperty.Register(
                "LabelsExceedBars",
                typeof(bool),
                typeof(Timeline),
                new PropertyMetadata(true));

        private void ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            svLabels.ScrollToHorizontalOffset(e.HorizontalOffset);
        }
    }
}
