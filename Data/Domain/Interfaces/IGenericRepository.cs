using PensionContManageSystem.Domain.DTOs;
using System.Linq.Expressions;
using System.Net.NetworkInformation;

namespace PensionContManageSystem.Domain.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
            Task<IList<T>> GetAllAsync(
                Expression<Func<T, bool>> expression = null,
                Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                List<string> includes = null
                );
           
            Task<T> Get(Expression<Func<T, bool>> expression, List<string> includes = null);
            Task insert(T entity); //Create
            Task insertRange(IEnumerable<T> entities);
            Task Delete(int id);//Delete
            void DeleteRange(IEnumerable<T> entities);
            void Update(T entity);//Update
        }
    }

