using PensionContManageSystem.Domain.DTOs.RequestDto;
using System.ComponentModel.DataAnnotations;

namespace PensionContManageSystem.Domain.DTOs
{
    public class MemberDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string DateOfBirth { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public ICollection<ContributionDto> Contributions { get; set; }
        [Required]
        [Range(18, 70, ErrorMessage = "Age must be between 18 and 70.")]
        public int Age { get; set; }
     
    }
}
