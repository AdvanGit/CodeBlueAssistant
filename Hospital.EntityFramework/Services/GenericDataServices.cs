using Hospital.Domain.Model;
using Hospital.Domain.Services;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Hospital.EntityFramework.Services
{
    public class GenericDataServices<T> : IDataServices<T> where T : DomainObject
    {
        private readonly HospitalDbContextFactory _contextFactory;

        public GenericDataServices(HospitalDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<T> Create(T entity)
        {
            using (HospitalDbContext db = _contextFactory.CreateDbContext())
            {
                var _entity = await db.Set<T>().AddAsync(entity);
                await db.SaveChangesAsync();
                return _entity.Entity;
            }
        }

        public async Task<bool> Delete(int id)
        {
            using (HospitalDbContext db = _contextFactory.CreateDbContext())
            {
                var entity = await db.Set<T>().AsQueryable().FirstOrDefaultAsync(e => e.Id == id);
                db.Set<T>().Remove(entity);
                await db.SaveChangesAsync();
                return true;
            }
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            using (HospitalDbContext db = _contextFactory.CreateDbContext())
            {
                return await db.Set<T>().AsQueryable().ToListAsync();
            }
        }

        public async Task<T> GetById(int id)
        {
            using (HospitalDbContext db = _contextFactory.CreateDbContext())
            {
                return await db.Set<T>().AsQueryable().AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
            }
        }

        public async Task<T> Update(int id, T entity)
        {
            using (HospitalDbContext db = _contextFactory.CreateDbContext())
            {
                entity.Id = id;
                db.Set<T>().Update(entity);
                await db.SaveChangesAsync();
                return entity;
            }
        }

        public async Task<IEnumerable<T>> GetWhere(Expression<Func<T, bool>> predicate)
        {
            using (HospitalDbContext db = _contextFactory.CreateDbContext())
            {
                return await db.Set<T>().AsQueryable().AsNoTracking().Where(predicate).ToListAsync();
            }
        }

        public async Task<IEnumerable<T>> GetWithInclude(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            using (HospitalDbContext db = _contextFactory.CreateDbContext())
            {
                return await Include(db.Set<T>(), includeProperties).Where(predicate).AsNoTracking().ToListAsync();
            }
        }

        private IQueryable<T> Include(IQueryable<T> query, params Expression<Func<T, object>>[] includeProperties)
        {
            return includeProperties.Aggregate(query, (current, includeProperty) => current.AsNoTracking().Include(includeProperty));
        }
    }
}
