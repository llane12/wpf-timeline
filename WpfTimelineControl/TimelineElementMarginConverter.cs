using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WpfTimelineControl
{
    public class TimelineElementMarginConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            double offsetScaled = 0;

            try
            {
                DateTime start = (DateTime)values[0];
                DateTime timelineStart = (DateTime)values[1];
                int timelineIntervalSeconds = (int)values[2];
                int timelinePixelsPerInterval = (int)values[3];
                TimelineEntryType entryType = (TimelineEntryType)values[4];
                int pointWidth = (int)values[5];

                TimeSpan offset = start - timelineStart;
                offsetScaled = offset.TotalSeconds / timelineIntervalSeconds * timelinePixelsPerInterval;

                if (entryType == TimelineEntryType.Point)
                {
                    offsetScaled -= pointWidth / 2;
                }
            }
            catch { }

            return new Thickness(offsetScaled, 5, 0, 0);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
