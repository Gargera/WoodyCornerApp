using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WoodyCornerApp.DAL.Entities;

namespace WoodyCornerApp.DAL
{
    public class WoodyCornerAppDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<CartItem> CartItems { get; set; }

        public WoodyCornerAppDbContext(DbContextOptions<WoodyCornerAppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Room>(entity =>
            {
                entity.HasKey(r => r.Id);

                entity.Property(r => r.Id)
                    .ValueGeneratedOnAdd();

                entity.Property(r => r.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(r => r.Description)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(r => r.ImagePath)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.HasMany(r => r.Products)
                    .WithOne(p => p.Room)
                    .HasForeignKey(p => p.RoomId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.ToTable(t => t.HasCheckConstraint("CK_Room_Name", "LEN([Name]) >= 2"));
                entity.ToTable(t => t.HasCheckConstraint("CK_Room_Description", "LEN([Description]) >= 10"));
            });

            builder.Entity<Product>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.Property(p => p.Id)
                    .ValueGeneratedOnAdd();

                entity.Property(p => p.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(p => p.Description)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(p => p.Price)
                    .IsRequired()
                    .HasColumnType("decimal(10,2)")
                    .HasPrecision(10, 2);

                entity.Property(p => p.StockQuantity)
                    .IsRequired()
                    .HasDefaultValue(0);

                entity.Property(p => p.ImagePath)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(p => p.RoomId)
                    .IsRequired();

                entity.HasOne(p => p.Room)
                    .WithMany(r => r.Products)
                    .HasForeignKey(p => p.RoomId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(p => p.CartItems)
                    .WithOne(c => c.Product)
                    .HasForeignKey(c => c.ProductId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(p => p.OrderItems)
                    .WithOne(oi => oi.Product)
                    .HasForeignKey(oi => oi.ProductId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.ToTable(t => t.HasCheckConstraint("CK_Product_Name", "LEN([Name]) >= 2"));
                entity.ToTable(t => t.HasCheckConstraint("CK_Product_Description", "LEN([Description]) >= 10"));
                entity.ToTable(t => t.HasCheckConstraint("CK_Product_StockQuantity", "[StockQuantity] >= 0 AND [StockQuantity] <= 1000000"));
            });

            builder.Entity<ApplicationUser>(entity =>
            {
                entity.Property(u => u.FullName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(u => u.Address)
                    .IsRequired()
                    .HasMaxLength(300);

                entity.Property(u => u.City)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasMany(u => u.CartItems)
                    .WithOne(c => c.User)
                    .HasForeignKey(c => c.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(u => u.Orders)
                    .WithOne(o => o.User)
                    .HasForeignKey(o => o.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.ToTable(t => t.HasCheckConstraint("CK_User_City", "LEN([City]) >= 3"));
                entity.ToTable(t => t.HasCheckConstraint("CK_User_Address", "LEN([Address]) >= 10"));
            });

            builder.Entity<CartItem>(entity =>
            {
                entity.HasKey(c => c.Id);

                entity.Property(c => c.Id)
                    .ValueGeneratedOnAdd();

                entity.Property(c => c.ProductId)
                    .IsRequired();

                entity.Property(c => c.UserId)
                    .IsRequired();

                entity.Property(c => c.Quantity)
                    .IsRequired()
                    .HasDefaultValue(1);

                entity.HasIndex(c => new { c.UserId, c.ProductId })
                      .IsUnique();

                entity.ToTable(t => t.HasCheckConstraint("CK_CartItem_Quantity", "[Quantity] >= 1 AND [Quantity] <= 100"));
            });

            builder.Entity<Order>(entity =>
            {
                entity.HasKey(o => o.Id);

                entity.Property(o => o.Id)
                    .ValueGeneratedOnAdd();

                entity.Property(o => o.UserId)
                    .IsRequired();

                entity.Property(o => o.OrderDate)
                    .IsRequired()
                    .HasDefaultValueSql("GETUTCDATE()");

                entity.Property(o => o.TotalPrice)
                    .IsRequired()
                    .HasColumnType("decimal(10,2)")
                    .HasPrecision(10, 2);

                entity.Property(o => o.OrderStatus)
                    .IsRequired()
                    .HasConversion<string>()
                    .HasMaxLength(20)
                    .HasDefaultValue(OrderStatus.Pending);

                entity.Property(o => o.ShippingAddress)
                    .IsRequired()
                    .HasMaxLength(300);

                entity.Property(o => o.ShippingCity)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasMany(o => o.OrderItems)
                    .WithOne(oi => oi.Order)
                    .HasForeignKey(oi => oi.OrderId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.ToTable(t => t.HasCheckConstraint("CK_Order_TotalPrice", "[TotalPrice] >= 0.01"));
                entity.ToTable(t => t.HasCheckConstraint("CK_Order_ShippingCity", "LEN([ShippingCity]) >= 3"));
                entity.ToTable(t => t.HasCheckConstraint("CK_Order_ShippingAddress", "LEN([ShippingAddress]) >= 10"));
            });

            builder.Entity<OrderItem>(entity =>
            {
                entity.HasKey(oi => oi.Id);

                entity.Property(oi => oi.Id)
                    .ValueGeneratedOnAdd();

                entity.Property(oi => oi.OrderId)
                    .IsRequired();

                entity.Property(oi => oi.ProductId)
                    .IsRequired();

                entity.Property(oi => oi.Quantity)
                    .IsRequired();

                entity.Property(oi => oi.PriceAtPurchase)
                    .IsRequired()
                    .HasColumnType("decimal(10,2)")
                    .HasPrecision(10, 2);

                entity.ToTable(t => t.HasCheckConstraint("CK_OrderItem_Quantity", "[Quantity] >= 1 AND [Quantity] <= 1000"));
            });

            base.OnModelCreating(builder);
        }
    }
}
