using Hospital.WPF.Commands;
using System;
using System.Globalization;
using System.Windows.Data;

namespace Hospital.WPF.Services
{
    class SearchExecuter : IMultiValueConverter
    {

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if ((int)values[0] == 0) return new Command(GetCompoundExecute(values[1]), GetCompoundCanExecute(values[1]));
            else return new Command(GetCompoundExecute(values[2]), GetCompoundCanExecute(values[2]));
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }

        private Action<object> GetCompoundExecute(object command)
        {
            return (parameter) =>
            {
                if (command != default(Command))
                    ((Command)command).Execute(parameter);
            };
        }

        private Func<object, bool> GetCompoundCanExecute(object command)
        {
            return (parameter) =>
            {
                bool res = true;
                if (command != default(Command))
                    res &= ((Command)command).CanExecute(parameter);
                return res;
            };
        }
    }
}
