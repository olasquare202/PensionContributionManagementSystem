using PensionContManageSystem.Utility;
using System.ComponentModel.DataAnnotations;

namespace PensionContManageSystem.Domain.DTOs.RequestDto
{
    public class GetMemberStatementRequestDto
    {
        [Required(ErrorMessage = "{0} is required")]
        [Range(1, int.MaxValue, ErrorMessage = "{0} must be greater than 0.")]
        public int MemberId { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [ValidDateRangeAttribute]
        public string StartDate { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        public string EndDate { get; set; }
    }
}
