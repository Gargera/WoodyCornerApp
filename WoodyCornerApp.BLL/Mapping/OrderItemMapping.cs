using System;
using System.Collections.Generic;
using System.Text;
using WoodyCornerApp.BLL.DTOs.OrderItemDtos;
using WoodyCornerApp.DAL.Entities;

namespace WoodyCornerApp.BLL.Mapping
{
    public static class OrderItemMapping
    {
        public static OrderItem EntityToOrderItem(this GetOrderItemDto getOrderItemDto)
        {
            return new OrderItem
            {

            };
        }
        public static GetOrderItemDto EntityToGetOrderItemDto(this OrderItem orderItem)
        {
            return new GetOrderItemDto
            {

            };
        }
    }
}
