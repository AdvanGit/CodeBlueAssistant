using Hospital.ViewModel.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;

namespace Hospital.WPF.Services
{
    class SearchLocatorConverter : IMultiValueConverter
    {

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if ( (int)values[0] == 0 ) return new RelayCommand(GetCompoundExecute(values[1]), GetCompoundCanExecute(values[1]));
            else return new RelayCommand(GetCompoundExecute(values[2]), GetCompoundCanExecute(values[2]));
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }

        private Action<object> GetCompoundExecute(object command)
        {
            return (parameter) =>
            {
                    if (command != default(RelayCommand))
                        ((RelayCommand)command).Execute(parameter);
            };
        }

        private Func<object, bool> GetCompoundCanExecute(object command)
        {
            return (parameter) =>
            {
                bool res = true;
                    if (command != default(RelayCommand))
                        res &= ((RelayCommand)command).CanExecute(parameter);
                return res;
            };
        }
    }
}
