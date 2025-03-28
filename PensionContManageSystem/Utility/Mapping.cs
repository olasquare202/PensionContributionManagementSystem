using AutoMapper;
using PensionContManageSystem.Domain.DTOs;
using PensionContManageSystem.Domain.DTOs.RequestDto;
using PensionContManageSystem.Domain.DTOs.ResponseDto;
using PensionContManageSystem.Domain.Entity;

namespace PensionContManageSystem.Utility
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Member, RegisterDto>().ReverseMap();
            CreateMap<UpdateMemberDetails, Member>().ReverseMap();
            CreateMap<RegisterDto, Employer>().ReverseMap();
            CreateMap<Employer, EmployerDto>().ReverseMap();
            CreateMap<MemberResponseDto, Member>().ReverseMap();
            CreateMap<EmployerResponseDto, Employer>().ReverseMap();
            CreateMap<ContributionResponseDto, Contribution>().ReverseMap();
            CreateMap<Contribution, ContributionDto>().ReverseMap();
        }
    }
}
