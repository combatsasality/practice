using System;
using System.Globalization;
using System.Windows.Data;

namespace DocumentWorker.Utils
{
    public class GuidConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Guid user)
            {
                return App.Data.Users.Find(u => u.Id == user).Username;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
