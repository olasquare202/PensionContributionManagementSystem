using PensionContManageSystem.Domain.DTOs.RequestDto;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace PensionContManageSystem.Utility
{
    public class ValidDateRangeAttribute : ValidationAttribute
    {
        private readonly string _dateFormat = "yyyy-MM-dd";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var model = (GetMemberStatementRequestDto)validationContext.ObjectInstance;
            string startDateStr = model.StartDate;
            string endDateStr = model.EndDate;

            // Validate format
            if (!DateTime.TryParseExact(startDateStr, _dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime startDate) ||
                !DateTime.TryParseExact(endDateStr, _dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime endDate))
            {
                return new ValidationResult($"Dates must be in the format '{_dateFormat}'.");
            }

            // Ensure start date is not in the future
            if (startDate > DateTime.Today)
            {
                return new ValidationResult("Start date cannot be in the future.");
            }

            // Ensure start date is less than or equal to end date
            if (startDate >= endDate)
            {
                return new ValidationResult("Start date must be lesser than the end date.");
            }

            return ValidationResult.Success;
        }
    }
}
