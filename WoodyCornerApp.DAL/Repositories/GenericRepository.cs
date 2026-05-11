using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using WoodyCornerApp.DAL.Entities;
using WoodyCornerApp.DAL.Interfaces;

namespace WoodyCornerApp.DAL.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity<int>
    {
        private readonly WoodyCornerAppDbContext _dbContext;

        public GenericRepository(WoodyCornerAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<TEntity> GetAllEntities()
        {
            return _dbContext.Set<TEntity>();
        }

        public IQueryable<TEntity> GetEntityById(int id)
        {
            return _dbContext.Set<TEntity>().Where(e => e.Id == id);
        }

        public async Task AddEntityAsync(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
        }

        public void UpdateEntity(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
        }

        public async Task DeleteEntityAsync(int id)
        {
            var entity = await _dbContext.Set<TEntity>().FindAsync(id);
            _dbContext.Set<TEntity>().Remove(entity!);
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbContext.Set<TEntity>().AnyAsync(predicate);
        }
    }
}
