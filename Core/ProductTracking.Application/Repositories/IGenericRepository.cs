using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracking.Application.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> GetAll(bool tracking = true);
        Task<T> GetByIdAsync(string id, bool tracking = true);
        Task<bool> AddAsync(T entity);
        Task<bool> AddRangeAsync(List<T> entities);
        bool Update(T entity);
        bool Remove(T entity);
        Task<bool> RemoveByIdAsync(string id);


        IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool tracking = true);
        Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true);
    }
}
