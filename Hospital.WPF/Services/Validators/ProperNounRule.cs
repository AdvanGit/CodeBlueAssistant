using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace Hospital.WPF.Services.Validators
{
    public class ProperNounRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (((string)value).Length == 0) return new ValidationResult(false, $"Поле не может быть пустым");
            else if (!Regex.IsMatch(value.ToString(), @"^[А-Я]\w*")) return new ValidationResult(false, "Поле должно начинаться с заглавной русской буквы");
            else if (!Regex.IsMatch(value.ToString(), @"^.[а-я]+$")) return new ValidationResult(false, "Поле должно содержать только русские буквы");

            return ValidationResult.ValidResult;
        }
    }
}
