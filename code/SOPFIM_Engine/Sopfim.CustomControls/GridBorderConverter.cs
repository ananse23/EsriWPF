using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Sopfim.CustomControls
{
    public class GridBorderConverter : IValueConverter 
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var black = new SolidColorBrush(Colors.Black);
            var red = new SolidColorBrush(Colors.Red);
            var isReadOnly = (bool) value;
            return isReadOnly ? black : red;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}