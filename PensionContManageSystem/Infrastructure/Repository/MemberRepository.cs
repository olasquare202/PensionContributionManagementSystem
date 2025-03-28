//using Microsoft.EntityFrameworkCore;
//using PensionContManageSystem.Domain;
//using PensionContManageSystem.Domain.DTOs;
//using PensionContManageSystem.Domain.Interfaces;

//namespace PensionContManageSystem.Infrastructure.Repository
//{
//    public class MemberRepository : IMemberRepository
//    {
//        private readonly AppDbContext _dbContext;

//        public MemberRepository(AppDbContext dbContext)
//        {
//            _dbContext = dbContext;
//        }
//        public ICollection<MemberDto> GetAllMemberAsync()
//        {
//            return _dbContext.members.OrderBy(x => x.FullName).ToList();
//        }

//        public Task<IEnumerable<MemberDto>> GetMemberByIdAsync(int id)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<MemberDto> RegisterMemberAsync(Member member)
//        {
//            throw new NotImplementedException();
//        }

//        public Task SoftDeleteMemberAsync(int id)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<MemberDto?> UpdateCouponAsync(MemberDto memberDto)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
