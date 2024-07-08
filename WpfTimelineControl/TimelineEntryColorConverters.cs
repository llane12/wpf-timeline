using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace WpfTimelineControl
{
    public class TimelineEntryForegroundConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                int colorIndex = (int)values[0];
                SolidColorBrush[] brushes = (SolidColorBrush[])values[1];

                return brushes[colorIndex];
            }
            catch { return Brushes.Black; }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class TimelineEntryBackgroundConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            SolidColorBrush foreground = Brushes.Black;

            try
            {
                int colorIndex = (int)values[0];
                SolidColorBrush[] brushes = (SolidColorBrush[])values[1];
                foreground = brushes[colorIndex];
            }
            catch { }

            var brushColor = foreground.Color;

            var colorInDrawing = System.Drawing.Color.FromArgb(brushColor.A, brushColor.R, brushColor.G, brushColor.B);
            ColorConverter.ColorToHsv(colorInDrawing, out double h, out _, out _);

            // Mimicking the same hue at 0.05% opacity over white, but opaque
            var backgroundInDrawing = ColorConverter.HsvToColor(h, 0.08, 0.98);
            return new SolidColorBrush(Color.FromArgb(backgroundInDrawing.A, backgroundInDrawing.R, backgroundInDrawing.G, backgroundInDrawing.B));
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
