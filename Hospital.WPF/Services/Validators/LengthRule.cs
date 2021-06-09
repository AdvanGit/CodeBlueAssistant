using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace Hospital.WPF.Services.Validators
{
    public class LengthRule : ValidationRule
    {
        private string message;
        
        private void MessageCheck()
        {
            if (Min != Max) message = $"Длинна строки должна состовлять от {Min} до {Max} символов";
            else message = $"Длинна строки должна составлять {Max} символов";
        }

        public int Min { get; set; }
        public int Max { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            MessageCheck();
            if ((value.ToString().Length < Min) || (value.ToString().Length > Max)) return new ValidationResult(false, message);
            else return ValidationResult.ValidResult;
        }
    }

    public class NumericLenghtRule : LengthRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (!Regex.IsMatch(value.ToString(), @"[1-9]+$")) return new ValidationResult(false, "Поле должно содержать только цифры");
            else return base.Validate(value, cultureInfo);
        }
    }
}
