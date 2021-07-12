using System;
using System.Globalization;
using System.Windows.Data;

namespace Hospital.WPF.Services.Converters
{
    public class ScheduleRowCountConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (double.TryParse(values[0].ToString(), out double totalWidth)
                && int.TryParse(values[1].ToString(), out int columnCount))
            {
                double result = (totalWidth / columnCount);
                if (result > 230) return result;
                else return 230.0;
            }
            else return 0.0;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
