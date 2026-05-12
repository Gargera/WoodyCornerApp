using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;
using WoodyCornerApp.BLL.DTOs.ProductDtos;
using WoodyCornerApp.BLL.DTOs.CartItemDtos;
using WoodyCornerApp.BLL.DTOs.OrderItemDtos;
using WoodyCornerApp.DAL.Entities;

namespace WoodyCornerApp.BLL.Mapping
{
    public static class ProductMapping
    {
        public static Product EntityToProduct(this CreateProductDto createProductDto)
        {
            return new Product
            {
                Name = createProductDto.Name,
                Description = createProductDto.Description,
                Price = createProductDto.Price,
                StockQuantity = createProductDto.StockQuantity,
                ImagePath = createProductDto.ImagePath,
                RoomId = createProductDto.RoomId
            };
        }

        public static Product EntityToProduct(this UpdateProductDto updateProductDto)
        {
            return new Product
            {
                Id = updateProductDto.Id,
                Name = updateProductDto.Name,
                Description = updateProductDto.Description,
                Price = updateProductDto.Price,
                StockQuantity = updateProductDto.StockQuantity,
                ImagePath = updateProductDto.ImagePath,
                RoomId = updateProductDto.RoomId
            };
        }

        public static GetProductDto EntityToGetProductDto(this Product product)
        {
            return new GetProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
                ImagePath = product.ImagePath,
                RoomId = product.RoomId,
                Room = product.Room.EntityToGetRoomDto(),
                CartItems = product.CartItems.Select(c => c.EntityToGetCartItemDto()).ToList(),
                OrderItems = product.OrderItems.Select(r => r.EntityToGetOrderItemDto()).ToList()
            };
        }
    }
}
