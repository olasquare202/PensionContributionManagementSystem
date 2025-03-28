namespace PensionContManageSystem.Domain.DTOs.ResponseDto
{
    public class StatementResponseDto
    {
        public int MemberId { get; set; }
        public decimal TotalAmountOfVoluntaryContribution { get; set; }
        public decimal TotalAmountOfMonthlyContribution { get; set; }
        public decimal TotalContribution { get; set; }
        public List<ContributionResponseDto> Contributions { get; set; }
        public string From { get; set; }
        public string To { get; set; }

    }
}
