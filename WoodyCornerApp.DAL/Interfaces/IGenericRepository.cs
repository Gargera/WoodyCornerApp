using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WoodyCornerApp.DAL.Entities;

namespace WoodyCornerApp.DAL.Interfaces
{
    public interface IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public Task<IEnumerable<TEntity>> GetAllEntitiesAsync();

        public Task<TEntity?> GetEntityByIdAsync(TKey id);

        public Task AddEntityAsync(TEntity entity);

        public void UpdateEntity(TEntity entity);

        public Task DeleteEntityAsync(TKey id);
    }
}
