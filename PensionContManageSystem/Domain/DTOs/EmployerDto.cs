using System.ComponentModel.DataAnnotations;

namespace PensionContManageSystem.Domain.DTOs
{
    public class EmployerDto
    {
        public int Id { get; set; }
        [Required]
        public string CompanyName { get; set; }
        [Required]
        public string RegistrationNumber { get; set; }
        //public bool IsActive { get; set; }
        //public List<Member> Members { get; set; }

    }
}
