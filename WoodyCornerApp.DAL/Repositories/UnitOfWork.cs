using System;
using System.Collections.Generic;
using System.Text;
using WoodyCornerApp.DAL.Entities;
using WoodyCornerApp.DAL.Interfaces;

namespace WoodyCornerApp.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly WoodyCornerAppDbContext _dbContext;
        public IGenericRepository<Room> Rooms { get; }
        public IGenericRepository<Product> Products { get; }
        public IGenericRepository<Order> Orders { get; }
        public IGenericRepository<OrderItem> OrderItems { get; }
        public IGenericRepository<CartItem> CartItems { get; }

        public UnitOfWork(WoodyCornerAppDbContext dbContext)
        {
            _dbContext = dbContext;
            Rooms = new GenericRepository<Room>(dbContext);
            Products = new GenericRepository<Product>(dbContext);
            Orders = new GenericRepository<Order>(dbContext);
            OrderItems = new GenericRepository<OrderItem>(dbContext);
            CartItems = new GenericRepository<CartItem>(dbContext);
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
