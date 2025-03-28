namespace PensionContManageSystem.Domain.DTOs.ResponseDto
{
    public class EmployerResponseDto
    {
        public string CompanyName { get; set; }
        public string RegistrationNumber { get; set; }
        public bool IsActive { get; set; } = true;

    }
}
