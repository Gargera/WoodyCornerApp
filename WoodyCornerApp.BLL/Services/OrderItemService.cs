using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WoodyCornerApp.BLL.Common;
using WoodyCornerApp.BLL.DTOs.OrderItemDtos;
using WoodyCornerApp.DAL.Entities;
using WoodyCornerApp.DAL.Interfaces;
using WoodyCornerApp.BLL.Interfaces;
using WoodyCornerApp.BLL.Mapping;

namespace WoodyCornerApp.BLL.Services
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderItemService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResult<GetOrderItemDto>> GetOrderItemByIdAsync(int id)
        {
            var orderItem = await _unitOfWork.OrderItems.GetEntityById(id)
                                                        .Include(oi => oi.Product)
                                                        .Include(oi => oi.Order)
                                                        .FirstOrDefaultAsync();

            var result = new ServiceResult<GetOrderItemDto>();

            if (orderItem != null)
            {
                result.Success = true;
                result.Message = "OrderItem Found";
                result.Data = orderItem.EntityToGetOrderItemDto();
            }
            else
            {
                result.Success = false;
                result.Message = "OrderItem NotFound!";
            }

            return result;
        }

        public async Task<ServiceResult<GetOrderItemDto>> DeleteOrderItemAsync(int id)
        {
            var result = await GetOrderItemByIdAsync(id);

            if (result.Success)
            {
                await _unitOfWork.OrderItems.DeleteEntityAsync(id);
                await _unitOfWork.SaveChangesAsync();
                result.Message = "OrderItem Deleted Successfully";
            }

            return result;
        }

        public async Task<ServiceResult<CreateOrderItemDto>> CreateOrderItemAsync(CreateOrderItemDto createOrderItemDto)
        {
            var findAny = await _unitOfWork.OrderItems.AnyAsync(ci => ci.ProductId == createOrderItemDto.ProductId && ci.OrderId == createOrderItemDto.OrderId);

            var result = new ServiceResult<CreateOrderItemDto>();
            if (findAny)
            {
                result.Success = false;
                result.Message = "An OrderItem with the same productId and orderId already exists";
            }
            else
            {
                if (createOrderItemDto.Quantity < 1 || createOrderItemDto.Quantity > 1000)
                {
                    result.Success = false;
                    result.Message = "Quantity must be between 1 and 1000";
                }
                else
                {
                    await _unitOfWork.OrderItems.AddEntityAsync(createOrderItemDto.EntityToOrderItem());
                    await _unitOfWork.SaveChangesAsync();
                    result.Success = true;
                    result.Message = "OrderItem Created Successfully";
                }
            }

            result.Data = createOrderItemDto;
            return result;
        }

        public async Task<IEnumerable<GetOrderItemDto>> GetAllOrderItemsAsync()
        {
            var orderItems = await _unitOfWork.OrderItems.GetAllEntities()
                                                        .Include(oi => oi.Product)
                                                        .Include(oi => oi.Order)
                                                        .ToListAsync();

            return orderItems.Select(oi => oi.EntityToGetOrderItemDto());
        }
    }
}
