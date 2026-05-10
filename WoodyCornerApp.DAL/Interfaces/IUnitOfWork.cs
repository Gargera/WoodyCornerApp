using System;
using System.Collections.Generic;
using System.Text;
using WoodyCornerApp.DAL.Entities;

namespace WoodyCornerApp.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        public IGenericRepository<Room, int> Rooms { get; }
        public IGenericRepository<Product, int> Products { get; }
        public IGenericRepository<Order, int> Orders { get; }
        public IGenericRepository<OrderItem, int> OrderItems { get; }
        public IGenericRepository<CartItem, int> CartItems { get; }

        public Task<int> SaveAsync();
    }
}
