using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WoodyCornerApp.BLL.Common;
using WoodyCornerApp.BLL.DTOs.OrderDtos;
using WoodyCornerApp.BLL.Interfaces;
using WoodyCornerApp.BLL.Mapping;
using WoodyCornerApp.DAL.Entities;
using WoodyCornerApp.DAL.Interfaces;
using WoodyCornerApp.BLL.Validation;

namespace WoodyCornerApp.BLL.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResult<GetOrderDto>> GetOrderByIdAsync(int id)
        {
            var order = await _unitOfWork.Orders.GetEntityById(id)
                                                .Include(o => o.OrderItems)
                                                .Include(o => o.User)
                                                .FirstOrDefaultAsync();

            var result = new ServiceResult<GetOrderDto>();

            if (order != null)
            {
                result.Success = true;
                result.Message = "Order Found";
                result.Data = order.EntityToGetOrderDto();
            }
            else
            {
                result.Success = false;
                result.Message = "Order NotFound!";
            }

            return result;
        }

        public async Task<ServiceResult<GetOrderDto>> DeleteOrderAsync(int id) //Admin Only
        {
            var result = await GetOrderByIdAsync(id);

            if (result.Success)
            {
                await _unitOfWork.Orders.DeleteEntityAsync(id);
                await _unitOfWork.SaveChangesAsync();
                result.Message = "Order Deleted Successfully";
            }

            return result;
        }

        public async Task<ServiceResult<CreateOrderDto>> CreateOrderAsync(CreateOrderDto createOrderDto) //SignedIn User Only
        {
            var result = new ServiceResult<CreateOrderDto>();
            
            var validationResult = createOrderDto.EntityToOrder().Valid();
            if (validationResult.valid)
            {
                await _unitOfWork.Orders.AddEntityAsync(createOrderDto.EntityToOrder());
                await _unitOfWork.SaveChangesAsync();
                result.Success = true;
                result.Message = "Order Created Successfully";
            }
            else
            {
                result.Success = false;
                result.Message = validationResult.message;
            }

            result.Data = createOrderDto;
            return result;
        }

        public async Task<IEnumerable<GetOrderDto>> GetAllOrdersAsync()
        {
            var orders = await _unitOfWork.Orders.GetAllEntities()
                                                 .Include(o => o.OrderItems)
                                                 .Include(o => o.User)
                                                 .ToListAsync();

            return orders.Select(o => o.EntityToGetOrderDto());
        }

        public async Task<ServiceResult<UpdateOrderDto>> UpdateOrder(UpdateOrderDto updateOrderDto)
        {
            var GetOrder = await GetOrderByIdAsync(updateOrderDto.Id);
            var result = new ServiceResult<UpdateOrderDto>();

            if (!GetOrder.Success)
            {
                result.Success = false;
                result.Message = "Order NotFound!";
            }
            else
            {
                var validationResult = updateOrderDto.EntityToOrder().Valid();
                if (validationResult.valid)
                {
                    _unitOfWork.Orders.UpdateEntity(updateOrderDto.EntityToOrder());
                    await _unitOfWork.SaveChangesAsync();
                    result.Message = "Order Updated Successfully";
                    result.Success = true;
                }
                else
                {
                    result.Success = false;
                    result.Message = validationResult.message;
                }
            }

            result.Data = updateOrderDto;
            return result;
        }
    }
}
