using System;
using System.Collections.Generic;
using System.Text;
using WoodyCornerApp.BLL.Common;
using WoodyCornerApp.BLL.DTOs.OrderDtos;

namespace WoodyCornerApp.BLL.Interfaces
{
    public interface IOrderService
    {
        public Task<ServiceResult<GetOrderDto>> GetOrderByIdAsync(int id);

        public Task<ServiceResult<GetOrderDto>> DeleteOrderAsync(int id);

        public Task<ServiceResult<CreateOrderDto>> CreateOrderAsync(CreateOrderDto createOrderDto);

        public Task<IEnumerable<GetOrderDto>> GetAllOrdersAsync();

        public Task<ServiceResult<UpdateOrderDto>> UpdateOrder(UpdateOrderDto updateOrderDto);
    }
}
