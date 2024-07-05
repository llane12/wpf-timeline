using System;
using System.Globalization;
using System.Windows.Data;

namespace WpfTimelineControl
{
    public class TimelineElementWidthConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            double widthScaled = 0;

            try
            {
                TimeSpan duration = (TimeSpan)values[0];
                int timelineIntervalSeconds = (int)values[1];
                int timelinePixelsPerInterval = (int)values[2];

                widthScaled = duration.TotalSeconds / timelineIntervalSeconds * timelinePixelsPerInterval;
            }
            catch { }

            return Math.Max(widthScaled, 1);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
