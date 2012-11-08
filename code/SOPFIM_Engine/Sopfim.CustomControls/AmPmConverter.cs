using System;
using System.Globalization;
using System.Windows.Data;

namespace Sopfim.CustomControls
{
    public class AmPmConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var date = value as DateTime?;
            if (date.HasValue)
            {
                return date.Value.Hour > 11 ? "PM" : "AM";
            }
            else
            {
                return "AM";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}