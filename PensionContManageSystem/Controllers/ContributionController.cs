using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PensionContManageSystem.Domain.DTOs.RequestDto;
using PensionContManageSystem.Domain.DTOs.ResponseDto;
using PensionContManageSystem.Domain.Entity;
using PensionContManageSystem.Domain.Enums;
using PensionContManageSystem.Domain.Interfaces;
using PensionContManageSystem.Infrastructure;

namespace PensionContManageSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContributionController : ControllerBase
    {
        private readonly IUniOfWork _uniOfWork;
        private readonly ILogger<MemberManagementController> _logger;
        private readonly IMapper _mapper;

        public ContributionController(IUniOfWork uniOfWork, ILogger<MemberManagementController> logger, IMapper mapper)
        {
            _uniOfWork = uniOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        /// <summary>
        /// Make Contribution Monthly or Voluntarily
        /// </summary>
        /// <param name="contributionType"></param>
        /// <param name="contributionDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ProcessContribution")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ProcessContribution( [FromQuery] ContributionType contributionType, [FromBody] ContributionDto contributionDto)
        {
            //Check if contribution has been made for the month, for monthly Contribution
            DateTime today = DateTime.Today;

            var isMemberExists = await _uniOfWork.members.Get(c => c.Id == contributionDto.MemberId);

            if (isMemberExists == null)
            {
                return BadRequest($"Invalid or Incorrect member Id. Member information was not found for {contributionDto.MemberId}.");
            }
            var hasContributedThisMonth = await _uniOfWork.contributions.Get(c => c.MemberId == contributionDto.MemberId && ( c.ContributionDate.Year == today.Year && c.ContributionDate.Month == today.Month) && c.Type == ContributionType.Monthly);
            if (hasContributedThisMonth != null && contributionType == ContributionType.Monthly)
            {
                return BadRequest($"Monthly contribution has already been made for member with Id {contributionDto.MemberId}.");
            }
            
                var contribution = new Contribution()
                {
                    Amount = contributionDto.Amount,
                    ContributionDate = DateTime.Now,
                    Type = contributionType,
                    MemberId = contributionDto.MemberId
                };
               
                    await _uniOfWork.contributions.insert(contribution);
                    await _uniOfWork.Save();


                return Ok($"{contributionType} contribution was successfully made for member with Id {contributionDto.MemberId}.");
            
        }
        /// <summary>
        /// Get member contribution by their Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}", Name = "GetMemberContributionsById")]
        [ResponseCache(CacheProfileName = "120SecondsDuration")]//Global method
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetMemberContributionsById(int id)
        {
            
                //throw new Exception();  //NOTE: uncomment this line to test the Global Error Handling
                var contributions = await _uniOfWork.contributions.Get(a => a.Id == id);
                var result = _mapper.Map<ContributionResponseDto>(contributions);
                if (result == null)
                {
                    return NotFound( new ContributionResponseDto());
                }
                return Ok(result);
            
        }
        /// <summary>
        /// Get member statement of contribution for range of times
        /// </summary>
        /// <param name="getMemberStatementRequestDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetMemberStatement")]
        [ResponseCache(CacheProfileName = "120SecondsDuration")]//Global method
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetMemberStatement(GetMemberStatementRequestDto getMemberStatementRequestDto)
        {
           
                DateTime startDate = DateTime.Parse(getMemberStatementRequestDto.StartDate); DateTime endDate = DateTime.Parse(getMemberStatementRequestDto.EndDate);
                var contributions = await _uniOfWork.contributions.GetAllAsync(a => a.MemberId == getMemberStatementRequestDto.MemberId && (a.ContributionDate >= startDate && a.ContributionDate <= endDate));
                var statementResponse = new StatementResponseDto
                {
                   MemberId = getMemberStatementRequestDto.MemberId,
                   TotalAmountOfVoluntaryContribution = contributions.Where(c => c.Type == ContributionType.Voluntary).Sum(c => c.Amount),
                   TotalAmountOfMonthlyContribution = contributions.Where(c => c.Type == ContributionType.Monthly).Sum(c => c.Amount),
                   TotalContribution = contributions.Sum(c => c.Amount),
                   Contributions = _mapper.Map<List<ContributionResponseDto>>(contributions),
                   From = getMemberStatementRequestDto.StartDate,
                   To = getMemberStatementRequestDto.EndDate
                };
                
                if (statementResponse == null)
                {
                    return NotFound(new StatementResponseDto());
                }
                return Ok(statementResponse);
        }
    }
}