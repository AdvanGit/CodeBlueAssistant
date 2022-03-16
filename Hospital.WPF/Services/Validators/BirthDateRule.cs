using System;
using System.Globalization;
using System.Windows.Controls;

namespace Hospital.WPF.Services.Validators
{
    public class BirthDateRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null) return new ValidationResult(false, $"Поле не может быть пустым");
            else if (((DateTime)value).Year < 1900 || ((DateTime)value > DateTime.Today)) return new ValidationResult(false, $"Дата рождения не верна");
            else return ValidationResult.ValidResult;
        }
    }
}
