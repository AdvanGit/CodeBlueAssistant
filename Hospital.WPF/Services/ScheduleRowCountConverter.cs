using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace Hospital.WPF.Services
{
    class ScheduleRowCountConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            double result;
            result = ((double)values[0] / (int)values[1]) - 25;
            if ((int)values[1] == 2) result -= 5;
            if ((int)values[1] == 1) result -= 10;
            if (result > 200) return result;
            else return (double)200;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
