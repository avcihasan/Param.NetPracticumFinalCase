using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ProductTracking.Application.Repositories;
using ProductTracking.Domain.Entities;
using ProductTracking.Persistence.Contexts;

namespace ProductTracking.Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly DbSet<T> _dbSet;

        public GenericRepository(ProductTrackingDbContext context)
        {
            _dbSet = context.Set<T>();
        }

        public async Task<bool> AddAsync(T entity)
        {
            EntityEntry<T> entityEntry = await _dbSet.AddAsync(entity);
            return entityEntry.State == EntityState.Added;
        }

        public async Task<bool> AddRangeAsync(List<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
            return true;
        }

        public IQueryable<T> GetAll(bool tracking = true)
        {
            IQueryable<T> entities = _dbSet.AsQueryable();
            if (!tracking)
                entities = entities.AsNoTracking();
            return entities;
        }

        public async Task<T> GetByIdAsync(string id, bool tracking = true)
        {
            IQueryable<T> entities = _dbSet.AsQueryable();
            if (!tracking)
                entities = entities.AsNoTracking();

            T entity = await entities.FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null)
                throw new Exception("Entity Bulunamadı!");
            return entity;
        }

        public bool Remove(T entity)
        {
            EntityEntry entityEntry = _dbSet.Remove(entity);
            return entityEntry.State == EntityState.Deleted;
        }

        public async Task<bool> RemoveByIdAsync(string id)
        {
            T entity = await GetByIdAsync(id);
            return Remove(entity);
        }

        public bool Update(T entity)
        {
            EntityEntry entityEntry = _dbSet.Update(entity);
            return entityEntry.State == EntityState.Modified;
        }
    }
}
