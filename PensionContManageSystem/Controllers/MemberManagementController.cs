using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PensionContManageSystem.Domain;
using PensionContManageSystem.Domain.DTOs;
using PensionContManageSystem.Domain.DTOs.RequestDto;
using PensionContManageSystem.Domain.DTOs.ResponseDto;
using PensionContManageSystem.Domain.Entity;
using PensionContManageSystem.Domain.Interfaces;
using PensionContManageSystem.Infrastructure;

namespace PensionContManageSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberManagementController : ControllerBase
    {
        private readonly IUniOfWork _uniOfWork;
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<MemberManagementController> _logger;
        private readonly IMapper _mapper;

        public MemberManagementController(IUniOfWork uniOfWork, AppDbContext appDbContext, ILogger<MemberManagementController> logger, IMapper mapper)
        {
            _uniOfWork = uniOfWork;
            _appDbContext = appDbContext;
            _logger = logger;
            _mapper = mapper;
        }
        /// <summary>
        /// Get list of member along with their contribution history and employer details
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        //[ResponseCache(Duration = 60)]//This is inline method for Cache
        [ResponseCache(CacheProfileName = "120SecondsDuration")]//Global method
        [Route("GetAllMember")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task <IActionResult> GetAllMember()
        {

            var members = await _uniOfWork.members.GetAllAsync(e => e.IsDeleted == false, includes: new List<string> { "Contributions", "Employers" });
            var results = _mapper.Map<ICollection<MemberResponseDto>>(members);//Mapping d Db data to DTO data
            return Ok(results);

            //I have configure Global Error Handling, I don't need ( try and catch )


            //try
            //{
            //    var members = await _uniOfWork.members.GetAllAsync(e => e.IsDeleted == false, includes: new List<string> { "Contributions", "Employers" });
            //    var results = _mapper.Map<ICollection<MemberResponseDto>>(members);//Mapping d Db data to DTO data
            //    return Ok(results);
            //}
            //catch (Exception ex)
            //{

            //    _logger.LogError(ex, $"Something went wrong in the {nameof(GetAllMember)}");
            //    return StatusCode(500, "Internal Server Error. Please Try Again Later");
            //}
        }

        /// <summary>
        /// Get a member along with his contribution history and employer details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:int}", Name = "GetMemberById")]
        [ResponseCache(CacheProfileName = "120SecondsDuration")]//Global method
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetMemberById (int id)
        {
            //try
            //{ 
                //throw new Exception();  //NOTE: uncomment this line to test the Global Error Handling
                var member = await _uniOfWork.members.Get(a => a.Id == id && a.IsDeleted == false, new List<string> { "Contributions", "Employers" });
                var result = _mapper.Map<MemberResponseDto>(member);
                if(result == null)
                {
                    return NotFound(new MemberResponseDto());
                }
                return Ok(result);
            //}
            //catch (Exception ex)
            //{

            //    _logger.LogError(ex, $"Something went wrong in the {nameof(GetMemberById)}");
            //    return StatusCode(500, "Internal Server Error. Please Try Again Later");
            //}
        }

        /// <summary>
        /// Register a new member along with his employer details
        /// </summary>
        /// <param name="registerDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("RegisterMember")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RegisterMember([FromBody] RegisterDto registerDto)
        {
            //Check if member exist in the Db
           
            var memberExists = await _uniOfWork.members.Get(m => m.FullName == registerDto.FullName);
            if (memberExists != null)
            {
                ModelState.AddModelError("Member", "This name already Exists");
            }
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid POST attempt in {nameof(RegisterMember)}");
                return BadRequest(ModelState);
            }
            //try
            //{
                // Check if employer already exists
                var employer = await _uniOfWork.employers.Get(e => e.RegistrationNumber == registerDto.RegistrationNumber);

                // If employer does not exist, create a new one
                if (employer == null)
                {
                     employer = _mapper.Map<Employer>(registerDto);
                
                   await _uniOfWork.employers.insert(employer);
                    await _uniOfWork.Save();
                }

                var member = _mapper.Map<Member>(registerDto);

                member.EmployerId = employer.Id;

                await _uniOfWork.members.insert(member);

                await _uniOfWork.Save();
                //This made it easy for frontend Dev to use the Route
                return CreatedAtRoute("GetMemberById", new { id = member.Id }, registerDto);
            //}
            //catch (Exception ex)
            //{

            //    _logger.LogError(ex, $"Something went wrong in the {nameof(RegisterMember)}");
            //    return StatusCode(500, "Internal Server Error. Please Try Again Later");
            //}
        }

        /// <summary>
        /// Update a member record
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateDetails"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("UpdateMemberDetails")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateMemberDetails(int id, [FromBody] UpdateMemberDetails updateDetails)
        {
            if (!ModelState.IsValid || id < 1)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateMemberDetails)}");
                return BadRequest(ModelState);
            }
            //try
            //{
                var editMemberDetails = await _uniOfWork.members.Get(m => m.Id == id);
                if (editMemberDetails == null)
                {
                    _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateMemberDetails)}");
                    return BadRequest("Submitted data is invalid");
                }
                _mapper.Map(updateDetails, editMemberDetails);
                _uniOfWork.members.Update(editMemberDetails);
                await _uniOfWork.Save();
                
                return NoContent();
            //}
            //catch (Exception ex)
            //{

            //    _logger.LogError(ex, $"Something went wrong in the {nameof(UpdateMemberDetails)}");
            //    return StatusCode(500, "Internal Server Error. Please Try Again Later");
            //}
        }

        /// <summary>
        /// SoftDelete: change a member status from active to inactive
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("SoftDeleteMember")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SoftDeleteMember(int id)
        {
            if(id < 1)
            {
                _logger.LogError($"Invali DELETE attempt in {nameof(SoftDeleteMember)}");
                return BadRequest(ModelState);
            }
            //try
            //{
                var removeMember = await _uniOfWork.members.Get(r => r.Id == id);
                if(removeMember == null)
                {
                    _logger.LogError($"Invalid  DELETE attempt in {nameof(SoftDeleteMember)}");
                    return BadRequest("Submitted data is invalid");
                }
                removeMember.IsDeleted = true;
                _uniOfWork.members.Update(removeMember);
                await _uniOfWork.Save();
                return NoContent();
            }
            //catch (Exception ex)
            //{

            //    _logger.LogError(ex, $"Something Went Wrong While Deleting Member in the {nameof(SoftDeleteMember)}");
            //    return StatusCode(500, "Internal Server Error. Please Try Again Later");
            //}
            
            
        //}
    }
}
