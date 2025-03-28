using Microsoft.EntityFrameworkCore;
using PensionContManageSystem.Domain.Entity;
using PensionContManageSystem.Domain.Interfaces;


namespace PensionContManageSystem.Infrastructure.Repository
{
    public class UnitOfWork : IUniOfWork
    {
        private readonly AppDbContext _dbContext;
        private IGenericRepository<Member> _members;
        private IGenericRepository<Employer> _employers;
        private IGenericRepository<Contribution> _contributions;

        public UnitOfWork(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IGenericRepository<Member> members => _members ??=new GenericRepository<Member>(_dbContext);

        public IGenericRepository<Employer> employers => _employers ??=new GenericRepository<Employer>(_dbContext);

        public IGenericRepository<Contribution> contributions => _contributions ??=new GenericRepository<Contribution>(_dbContext);

        public void Dispose()
        {
            _dbContext.Dispose();//dispose d memory after d operation
            GC.SuppressFinalize(this);//GC means Garbage Collector
        }

        public async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
