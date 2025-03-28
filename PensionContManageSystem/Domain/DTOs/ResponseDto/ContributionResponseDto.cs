using PensionContManageSystem.Domain.Enums;

namespace PensionContManageSystem.Domain.DTOs.ResponseDto
{
    public class ContributionResponseDto
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime ContributionDate { get; set; }
        public ContributionType Type { get; set; }
      
    }
}
