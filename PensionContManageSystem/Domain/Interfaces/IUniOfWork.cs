using PensionContManageSystem.Domain.DTOs;
using PensionContManageSystem.Domain.Entity;
using System.Diagnostics.Metrics;

namespace PensionContManageSystem.Domain.Interfaces
{
    public interface IUniOfWork : IDisposable
    {
        IGenericRepository<Member> members { get; }
        IGenericRepository<Employer> employers { get; }
        IGenericRepository<Contribution> contributions { get; }
        Task Save();
    }
}
