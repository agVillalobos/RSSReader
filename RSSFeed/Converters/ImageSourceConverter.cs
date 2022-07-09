using System;
using System.Globalization;
using Xamarin.Forms;

namespace RSSFeed.Converters
{
    public class ImageSourceConverter: IValueConverter
    {
        private const string defaultNotFoundImage = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSUrgu4a7W_OM8LmAuN7Prk8dzWXm7PVB_FmA&usqp=CAU";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is string)) return value;

            var src = value as string;
            var isGif = src.Contains(".gif");

            if (src == string.Empty || isGif) return defaultNotFoundImage;

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
