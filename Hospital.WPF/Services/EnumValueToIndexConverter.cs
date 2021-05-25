using System;
using System.Globalization;
using System.Windows.Data;

namespace Hospital.WPF.Services
{
    public class EnumValueToIndexConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            int index = (int)Enum.Parse(value.GetType(), value.ToString());
            return index;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
