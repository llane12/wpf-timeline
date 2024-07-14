using System.Windows;

namespace WpfTimelineControl
{
    public partial class Timeline
    {
        public Timeline()
        {
            InitializeComponent();
            DataContext = this;
        }

        public TimelineData TimelineData
        {
            get { return (TimelineData)GetValue(TimelineDataProperty); }
            set { SetValue(TimelineDataProperty, value); }
        }

        public static readonly DependencyProperty TimelineDataProperty =
            DependencyProperty.Register("TimelineData", typeof(TimelineData), typeof(Timeline), new PropertyMetadata(new TimelineData()));
    }
}
