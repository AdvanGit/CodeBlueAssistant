using Hospital.Domain.Model;
using Hospital.Domain.Services;
using Microsoft.EntityFrameworkCore;
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
                await db.Set<T>().AsQueryable().FirstOrDefaultAsync(e => e.Id == id);
                await db.SaveChangesAsync();
                return true;
            }
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            using (HospitalDbContext db = _contextFactory.CreateDbContext())
            {
                IEnumerable<T> entities = await db.Set<T>().AsQueryable().ToListAsync();
                return entities;
            }
        }

        public async Task<T> GetById(int id)
        {
            using (HospitalDbContext db = _contextFactory.CreateDbContext())
            {
                var entity = await db.Set<T>().AsQueryable().FirstOrDefaultAsync(e => e.Id == id);
                return entity;
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

        public async Task<IEnumerable<T>> GetWhere(Func<T, bool> predicate)
        {
            using (HospitalDbContext db = _contextFactory.CreateDbContext())
            {
                var entity = await db.Set<T>().AsQueryable().Where(predicate).ToAsyncEnumerable().ToListAsync();
                return entity;
            }
        }

        public IEnumerable<T> GetWithInclude(Func<T, bool> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            using (HospitalDbContext db = _contextFactory.CreateDbContext())
            {
                var query = Include(includeProperties);
                return query.Where(predicate).ToList();
            }
        }

        private IQueryable<T> Include(params Expression<Func<T, object>>[] includeProperties)
        {
            using (HospitalDbContext db = _contextFactory.CreateDbContext())
            {
                IQueryable<T> query = db.Set<T>();
                return includeProperties
                    .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            }
        }
    }
}
