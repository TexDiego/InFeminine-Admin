using System.Globalization;

namespace InFeminine_Admin.Helpers
{
    public class BoolToScaleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => (bool)value ? -1 : 1;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => (double)value == -1;
    }
}