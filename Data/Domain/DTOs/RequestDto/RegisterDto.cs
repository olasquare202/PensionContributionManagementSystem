using PensionContManageSystem.Utility;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text.Json.Serialization;

namespace PensionContManageSystem.Domain.DTOs.RequestDto
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "{0} is required")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [RegularExpression(@"\d{2}-\d{2}-\d{4}", ErrorMessage = "{0} must be in the format 'dd-MM-yyyy'.")]
        [CustomDateOfBirthValidation(ErrorMessage = "{0} must be a valid date in 'dd-MM-yyyy' format.")]
        public string DateOfBirth { get; set; }
        [JsonIgnore]
        public int Age
        {
            get
            {
                if (DateTime.TryParseExact(DateOfBirth, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dob))
                {
                    int age = DateTime.Today.Year - dob.Year;
                    if (dob.Date > DateTime.Today.AddYears(-age)) age--; // Adjust if birthday hasn’t occurred this year
                    return age;
                }
                return 0; // Default value if DateOfBirth is invalid
            }
        }


        [Required(ErrorMessage = "Phone number is required.")]
        [RegularExpression(@"^(234\d{10})$", ErrorMessage = "Please use only numbers and a valid format (e.g., 2348139490503).")]

        public string Phone { get; set; }


        [Required]
        public string Email { get; set; }


        [Required]
        public string CompanyName { get; set; }

        [Required]
        public string RegistrationNumber { get; set; }


    }
}
