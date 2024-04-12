using System;
using System.Globalization;
using System.Windows.Data;

namespace practice.Utils
{
    public class ExpiredTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return false;
            }
            if (value is DateTime time)
            {
                return time < DateTime.Now;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
