using PensionContManageSystem.Domain.Entity;
using PensionContManageSystem.Domain.Enums;
using System.Globalization;

namespace PensionContManageSystem.Domain.DTOs.ResponseDto
{
    public class MemberResponseDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string DateOfBirth { get; set; }
        public string Phone { get; set; }
        public int Age { get; set; }

        public MemberStatus MemberStatus { get; set; }

        public DateTime CreatedAt { get; set; } 

        public ICollection<ContributionResponseDto> Contributions { get; set; }
        
        public EmployerResponseDto Employers { get; set; }
    }
}
