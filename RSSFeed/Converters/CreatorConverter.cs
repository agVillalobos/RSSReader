using System;
using System.Globalization;
using Xamarin.Forms;

namespace RSSFeed.Converters
{
    public class CreatorConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var creator = (string)value;

            if (creator == null || creator == string.Empty) return string.Empty;

            creator = "by " + creator;

            return creator;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
