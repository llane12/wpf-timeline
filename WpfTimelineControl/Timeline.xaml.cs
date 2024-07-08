using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfTimelineControl
{
    public partial class Timeline
    {
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

        protected bool LabelsExceedBars
        {
            get { return (bool)GetValue(LabelsExceedBarsProperty); }
            set { SetValue(LabelsExceedBarsProperty, value); }
        }

        public static readonly DependencyProperty TimelineEntryBrushesProperty = DependencyProperty.Register(
                "TimelineEntryBrushes",
                typeof(SolidColorBrush[]),
                typeof(Timeline));

        public static readonly DependencyProperty NameLabelProperty = DependencyProperty.Register(
                "NameLabel",
                typeof(string),
                typeof(Timeline),
                new PropertyMetadata("Name"));

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
