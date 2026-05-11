using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using WoodyCornerApp.DAL.Entities;
using WoodyCornerApp.DAL.Interfaces;

namespace WoodyCornerApp.DAL.Repositories
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly WoodyCornerAppDbContext _dbContext;

        public GenericRepository(WoodyCornerAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<TEntity>> GetAllEntitiesAsync()
        {
            return await _dbContext.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity?> GetEntityByIdAsync(TKey id)
        {
            return await _dbContext.Set<TEntity>().FindAsync(id);
        }

        public async Task AddEntityAsync(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
        }

        public void UpdateEntity(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
        }

        public async Task DeleteEntityAsync(TKey id)
        {
            var entity = await GetEntityByIdAsync(id);
            _dbContext.Set<TEntity>().Remove(entity);
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbContext.Set<TEntity>().AnyAsync(predicate);
        }
    }
}
