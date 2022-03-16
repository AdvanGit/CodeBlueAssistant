using System;
using System.Globalization;
using System.Windows.Data;

namespace Hospital.WPF.Services.Converters
{
    public class DateToAgeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateOnly today = DateOnly.FromDateTime(DateTime.Now);
            DateOnly birthday = (DateOnly)value;

            int years = today.Year - birthday.Year;
            int month = today.Month - birthday.Month;

            if (today.Day < birthday.Day) month--;
            if (month < 0) years--;

            return years;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
