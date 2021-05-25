using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;

namespace Hospital.WPF.Services
{
    public class DiameterToLenghtConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length < 2 || !double.TryParse(values[0].ToString(), out double diameter) ||
                !double.TryParse(values[1].ToString(), out double thickness)) return 0;
            double res = diameter * Math.PI/thickness;

            if (parameter != null && bool.TryParse(parameter.ToString(), out bool result) && result) return -res;
            else return new DoubleCollection(new[] { res, res });
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
