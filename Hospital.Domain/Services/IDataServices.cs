using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Hospital.Domain.Services
{
    public interface IDataServices<T>
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> Create(T entity);
        Task<T> GetById(int id);
        Task<T> Update(int id, T entity);
        Task<bool> Delete(int id);

        Task<IEnumerable<T>> GetWhere(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetWithInclude(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        Task<T> GetItemWithInclude(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
    }
}
