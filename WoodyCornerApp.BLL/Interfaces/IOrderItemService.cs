using System;
using System.Collections.Generic;
using System.Text;
using WoodyCornerApp.BLL.Common;
using WoodyCornerApp.BLL.DTOs.OrderItemDtos;

namespace WoodyCornerApp.BLL.Interfaces
{
    public interface IOrderItemService
    {
        public Task<ServiceResult<GetOrderItemDto>> GetOrderItemByIdAsync(int id);
        
        public Task<ServiceResult<GetOrderItemDto>> DeleteOrderItemAsync(int id);
        
        public Task<ServiceResult<CreateOrderItemDto>> CreateOrderItemAsync(CreateOrderItemDto createOrderItemDto);
        
        public Task<IEnumerable<GetOrderItemDto>> GetAllOrderItemsAsync();
    }
}
