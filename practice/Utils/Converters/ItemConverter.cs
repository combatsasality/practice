using practice.Utils.DataStructures;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Collections.Generic;
using System.Linq;

namespace practice.Utils
{
    public class ItemConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is List<Guid> users && users.Count > 0)
            {
                List<User> usersList = App.Data.Users.FindAll(u => users.Contains(u.Id));
                return string.Join(", ", usersList.Select(u => u.Username));
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
