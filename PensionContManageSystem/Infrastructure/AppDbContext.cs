using Microsoft.EntityFrameworkCore;
using PensionContManageSystem.Domain.Entity;

namespace PensionContManageSystem.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) 
        {
            
        }
        public DbSet<Member> members { get; set; }
        public DbSet<Employer> employers { get; set; }
        public DbSet<Contribution> contributions { get; set; }
    }
}
