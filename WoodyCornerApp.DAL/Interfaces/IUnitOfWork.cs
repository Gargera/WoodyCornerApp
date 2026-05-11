using System;
using System.Collections.Generic;
using System.Text;
using WoodyCornerApp.DAL.Entities;

namespace WoodyCornerApp.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        public IGenericRepository<Room> Rooms { get; }
        public IGenericRepository<Product> Products { get; }
        public IGenericRepository<Order> Orders { get; }
        public IGenericRepository<OrderItem> OrderItems { get; }
        public IGenericRepository<CartItem> CartItems { get; }
        public Task<int> SaveAsync();
    }
}
