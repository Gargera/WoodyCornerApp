using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using WoodyCornerApp.DAL.Entities;

namespace WoodyCornerApp.DAL.Interfaces
{
    public interface IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public IQueryable<TEntity> GetAllEntities();

        public Task<TEntity?> GetEntityByIdAsync(TKey id);

        public Task AddEntityAsync(TEntity entity);

        public void UpdateEntity(TEntity entity);

        public Task DeleteEntityAsync(TKey id);

        public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);
    }
}