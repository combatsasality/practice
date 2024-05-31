using System;
using System.Globalization;
using System.Windows.Data;

namespace DocumentWorker.Utils
{
    /// <summary>
    /// Converter for DataGrid height
    /// </summary>
    public class DataGridHeightConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double height)
            {
                return height - 100;
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
