using Hospital.Domain.Model;
using System;
using System.Globalization;
using System.Windows.Data;

namespace Hospital.UI.Services
{
    class RowDisablingConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((EntryStatus)value == EntryStatus.Await)
            {
                return true;
            }
            else return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }
}
