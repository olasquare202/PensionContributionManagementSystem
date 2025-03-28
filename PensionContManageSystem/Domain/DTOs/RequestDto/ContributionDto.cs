using PensionContManageSystem.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace PensionContManageSystem.Domain.DTOs.RequestDto
{
    public class ContributionDto
    {
        [Required(ErrorMessage = "{0} is required")]
        public int MemberId { get; set; }


        [Required(ErrorMessage = "{0} is required")]
        [Range(1, double.MaxValue, ErrorMessage = "Amount must be greater than 0.")]
        public decimal Amount { get; set; }
        
    }
}
