using System;
using System.Globalization;
using System.Windows.Data;

namespace Hospital.WPF.Services
{
    class DateToAgeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime today = DateTime.Today;
            DateTime birthday = (DateTime)value;

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
