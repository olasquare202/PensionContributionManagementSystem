using PensionContManageSystem.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace PensionContManageSystem.Domain.Entity
{
    public class Contribution
    {
        [Key]
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime ContributionDate { get; set; }
        public ContributionType Type { get; set; }
        public int MemberId { get; set; }
        public Member Member { get; set; }
    }
}
