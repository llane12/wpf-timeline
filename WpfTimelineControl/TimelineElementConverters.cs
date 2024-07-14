using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WpfTimelineControl
{
    public static class TimelineConstants
    {
        /// <summary>
        /// Affects the size of the timeline on-screen
        /// </summary>
        public static double PixelsPerTimeUnit = 3;

        /// <summary>
        /// If the total duration is x times greater than the current interval, use the next one up
        /// </summary>
        public static int TimelineScalingFactor = 10;

        /// <summary>
        /// The major interval options available to select from, based on the total duration of the events on the timeline.
        /// Unit is minutes.
        /// </summary>
        public static readonly int[] MajorIntervalOptions = new int[] { 1, 10, 30, 60 };
    }

    public class TimelineElementMarginConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime start = (DateTime)values[0];
            DateTime timelineStart = (DateTime)values[1];
            int timelineIntervalMinutes = (int)values[2];

            TimeSpan offset = start - timelineStart;
            double offsetScaled = offset.TotalSeconds / timelineIntervalMinutes * TimelineConstants.PixelsPerTimeUnit;

            return new Thickness(offsetScaled, 0, 0, 0);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class TimelineElementWidthConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            TimeSpan duration = (TimeSpan)values[0];
            int timelineIntervalMinutes = (int)values[1];

            double widthScaled = duration.TotalSeconds / timelineIntervalMinutes * TimelineConstants.PixelsPerTimeUnit;

            return widthScaled;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
