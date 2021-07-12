using Hospital.WPF.Commands;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;

namespace Hospital.WPF.Services.Converters
{
    public class MultiCommandExecuter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            List<object> _commands = new List<object>(values);
            return new Command(GetCompoundExecute(_commands), GetCompoundCanExecute(_commands));
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }

        private Action<object> GetCompoundExecute(List<object> _commands)
        {
            return (parameter) =>
            {
                foreach (Command command in _commands)
                {
                    if (command != default(Command))
                        command.Execute(parameter);
                }
            };
        }

        private Func<object, bool> GetCompoundCanExecute(List<object> _commands)
        {
            return (parameter) =>
            {
                bool res = true;
                foreach (Command command in _commands)
                    if (command != default(Command))
                        res &= command.CanExecute(parameter);
                return res;
            };
        }
    }
}
