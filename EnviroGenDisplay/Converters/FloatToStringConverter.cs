using System;
using System.Globalization;
using System.Windows.Data;

namespace EnviroGenDisplay.Converters
{
    [ValueConversion(typeof(float), typeof(string))]
    class FloatToStringConverter : BaseConverter, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return float.Parse(value.ToString());
        }
    }
}
