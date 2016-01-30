using System;
using System.Globalization;
using System.Windows.Data;

namespace EnviroGenDisplay.Converters
{
    [ValueConversion(typeof(bool), typeof(string))]
    public class BoolToStringConverter : BaseConverter, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool b;

            if (bool.TryParse(value.ToString(), out b))
            {
                return b;
            }

            throw new ArgumentException("Not able to convert bool to string.");
        }
    }
}
