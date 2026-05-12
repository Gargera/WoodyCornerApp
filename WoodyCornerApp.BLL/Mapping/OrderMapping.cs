using System;
using System.Collections.Generic;
using System.Text;
using WoodyCornerApp.BLL.DTOs.OrderDtos;
using WoodyCornerApp.DAL.Entities;

namespace WoodyCornerApp.BLL.Mapping
{
    public static class OrderMapping
    {
        public static GetOrderDto EntityToGetOrderDto(this Order order)
        {
            return new GetOrderDto
            {
                Id = order.Id,
                UserId = order.UserId,
                OrderDate = order.OrderDate,
                TotalPrice = order.TotalPrice,
                OrderStatus = order.OrderStatus,
                ShippingAddress = order.ShippingAddress,
                ShippingCity = order.ShippingCity,
                User = order.User,
                OrderItems = order.OrderItems.Select(oi => oi.EntityToGetOrderItemDto()).ToList()
            };
        }

        public static Order EntityToOrder(this CreateOrderDto createOrderDto)
        {
            return new Order
            {
                UserId = createOrderDto.UserId,
                OrderDate = createOrderDto.OrderDate,
                TotalPrice = createOrderDto.TotalPrice,
                OrderStatus = createOrderDto.OrderStatus,
                ShippingAddress = createOrderDto.ShippingAddress,
                ShippingCity = createOrderDto.ShippingCity
            };
        }

        public static Order EntityToOrder(this UpdateOrderDto updateOrderDto)
        {
            return new Order
            {
                Id = updateOrderDto.Id,
                UserId = updateOrderDto.UserId,
                OrderDate = updateOrderDto.OrderDate,
                TotalPrice = updateOrderDto.TotalPrice,
                OrderStatus = updateOrderDto.OrderStatus,
                ShippingAddress = updateOrderDto.ShippingAddress,
                ShippingCity = updateOrderDto.ShippingCity
            };
        }
    }
}
