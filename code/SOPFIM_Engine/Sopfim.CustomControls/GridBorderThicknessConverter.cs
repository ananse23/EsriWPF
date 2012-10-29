using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Sopfim.CustomControls
{
    public class GridBorderThicknessConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var isReadOnly = (bool) value;
            var black = new Thickness(1);
            var red = new Thickness(4);
            return isReadOnly ? black : red;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}