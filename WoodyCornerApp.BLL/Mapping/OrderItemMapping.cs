using System;
using System.Collections.Generic;
using System.Text;
using WoodyCornerApp.BLL.DTOs.OrderItemDtos;
using WoodyCornerApp.DAL.Entities;

namespace WoodyCornerApp.BLL.Mapping
{
    public static class OrderItemMapping
    {
        public static GetOrderItemDto EntityToGetOrderItemDto(this OrderItem orderItem)
        {
            return new GetOrderItemDto
            {
                Id = orderItem.Id,
                OrderId = orderItem.OrderId,
                ProductId = orderItem.ProductId,
                Quantity = orderItem.Quantity,
                PriceAtPurchase = orderItem.PriceAtPurchase,
                Order = orderItem.Order,
                Product = orderItem.Product
            };
        }

        public static OrderItem EntityToOrderItem(this CreateOrderItemDto createOrderItemDto)
        {
            return new OrderItem
            {
                OrderId = createOrderItemDto.OrderId,
                ProductId = createOrderItemDto.ProductId,
                Quantity = createOrderItemDto.Quantity,
                PriceAtPurchase = createOrderItemDto.PriceAtPurchase
            };
        }
    }
}
