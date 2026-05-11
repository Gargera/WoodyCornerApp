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
        public static CreateProductDto EntityToCreateProductDto(this Product product)
        {
            return new CreateProductDto
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
                ImagePath = product.ImagePath,
                RoomId = product.RoomId
            };
        }

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

        public static UpdateProductDto EntityToUpdateProductDto(this Product product)
        {
            return new UpdateProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
                ImagePath = product.ImagePath,
                RoomId = product.RoomId
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
                Room = product.Room,
                CartItems = product.CartItems.Select(c => c.EntityToGetCartItemDto()).ToList(),
                OrderItems = product.OrderItems.Select(r => r.EntityToGetOrderItemDto()).ToList()
            };
        }

        public static Product EntityToProduct(this GetProductDto getProductDto)
        {
            return new Product
            {
                Id = getProductDto.Id,
                Name = getProductDto.Name,
                Description = getProductDto.Description,
                Price = getProductDto.Price,
                StockQuantity = getProductDto.StockQuantity,
                ImagePath = getProductDto.ImagePath,
                RoomId = getProductDto.RoomId
            };
        }
    }
}
