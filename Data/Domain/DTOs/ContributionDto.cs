namespace PensionContManageSystem.Domain.DTOs
{
    public class ContributionDto
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public double Amount { get; set; }
    }
}
