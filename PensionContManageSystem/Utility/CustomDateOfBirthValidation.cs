using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace PensionContManageSystem.Utility
{
    public class CustomDateOfBirthValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is string dob && !string.IsNullOrEmpty(dob))
            {
                if (DateTime.TryParseExact(dob, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
                {
                    int age = CalculateAge(parsedDate);
                    if (age < 18 || age > 70)
                    {
                        return new ValidationResult("Age must be between 18 and 70 years old.");
                    }
                    return ValidationResult.Success;
                }
                return new ValidationResult($"{validationContext.DisplayName} must be a valid date in 'dd-MM-yyyy' format.");
            }
            return new ValidationResult($"{validationContext.DisplayName} is required.");
        }

        private int CalculateAge(DateTime birthDate)
        {
            var today = DateTime.Today;
            int age = today.Year - birthDate.Year;
            if (birthDate.Date > today.AddYears(-age)) age--; // Adjust for birthdays not yet reached in the current year
            return age;
        }
    }
}