using System;
using System.Globalization;
using Xamarin.Forms;

namespace RSSFeed.Converters
{
    public class DateTimeConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var date = (DateTime)value;

            if (date == DateTime.MinValue) return string.Empty;

            return date.ToString("MMMM dd, yyyy");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
