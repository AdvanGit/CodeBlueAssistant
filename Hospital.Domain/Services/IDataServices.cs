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
    }
}
