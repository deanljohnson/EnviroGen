using System;
using System.Globalization;
using System.Windows.Data;
using SFML.Graphics;

namespace EnviroGenDisplay.Converters
{
    [ValueConversion(typeof(Color), typeof(System.Windows.Media.Color))]
    class SFMLColorToSystemColorConverter : BaseConverter, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var sfmlColor = (Color)value;
            return System.Windows.Media.Color.FromArgb(sfmlColor.A, sfmlColor.R, sfmlColor.G, sfmlColor.B);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var sysColor = (System.Windows.Media.Color)value;
            return new Color(sysColor.R, sysColor.G, sysColor.B, sysColor.A);
        }
    }
}
