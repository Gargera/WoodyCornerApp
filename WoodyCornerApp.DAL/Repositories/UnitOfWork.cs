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
        public IGenericRepository<Room, int> Rooms { get; }
        public IGenericRepository<Product, int> Products { get; }
        public IGenericRepository<Order, int> Orders { get; }
        public IGenericRepository<OrderItem, int> OrderItems { get; }
        public IGenericRepository<CartItem, int> CartItems { get; }

        public UnitOfWork(WoodyCornerAppDbContext dbContext)
        {
            _dbContext = dbContext;
            Rooms = new GenericRepository<Room, int>(dbContext);
            Products = new GenericRepository<Product, int>(dbContext);
            Orders = new GenericRepository<Order, int>(dbContext);
            OrderItems = new GenericRepository<OrderItem, int>(dbContext);
            CartItems = new GenericRepository<CartItem, int>(dbContext);
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public async Task<int> SaveAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
