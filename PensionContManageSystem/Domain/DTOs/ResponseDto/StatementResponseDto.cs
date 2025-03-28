namespace PensionContManageSystem.Domain.DTOs.ResponseDto
{
    public class StatementResponseDto
    {
        public int MemberId { get; set; }
        public double TotalAmountOfVoluntaryContribution { get; set; }
        public double TotalAmountOfMonthlyContribution { get; set; }
        public double TotalContribution { get; set; }
        public List<ContributionResponseDto> Contributions { get; set; }
        public string From { get; set; }
        public string To { get; set; }

    }
}
