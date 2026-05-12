using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WoodyCornerApp.BLL.Common;
using WoodyCornerApp.BLL.DTOs.CartItemDtos;
using WoodyCornerApp.BLL.DTOs.RoomDtos;
using WoodyCornerApp.BLL.Mapping;
using WoodyCornerApp.DAL.Entities;
using WoodyCornerApp.DAL.Interfaces;

namespace WoodyCornerApp.BLL.Services
{
    public class CartItemService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CartItemService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResult<GetCartItemDto>> GetCartItemByIdAsync(int id)
        {
            var cartItem = await _unitOfWork.CartItems.GetEntityById(id)
                                                      .Include(ci => ci.Product)
                                                      .Include(ci => ci.User)
                                                      .FirstOrDefaultAsync();

            var result = new ServiceResult<GetCartItemDto>();

            if (cartItem != null)
            {
                result.Success = true;
                result.Message = "CartItem Found";
                result.Data = cartItem.EntityToGetCartItemDto();
            }
            else
            {
                result.Success = false;
                result.Message = "CartItem NotFound!";
            }

            return result;
        }

        public async Task<ServiceResult<GetCartItemDto>> DeleteCartItemAsync(int id)
        {
            var result = await GetCartItemByIdAsync(id);

            if (result.Success)
            {
                await _unitOfWork.CartItems.DeleteEntityAsync(id);
                result.Message = "CartItem Deleted Successfully";
            }

            return result;
        }

        public async Task<ServiceResult<CreateCartItemDto>> CreateCartItemAsync(CreateCartItemDto createCartItemDto)
        {
            var findAny = await _unitOfWork.CartItems.AnyAsync(ci => ci.ProductId == createCartItemDto.ProductId && ci.UserId == createCartItemDto.UserId);

            var result = new ServiceResult<CreateCartItemDto>();
            if (findAny)
            {
                result.Success = false;
                result.Message = "A CartItem with the same productId and userId already exists";
            }
            else
            {
                if (createCartItemDto.Quantity < 1 || createCartItemDto.Quantity > 100)
                {
                    result.Success = false;
                    result.Message = "Quantity must be between 1 and 100";
                }
                else
                {
                    await _unitOfWork.CartItems.AddEntityAsync(createCartItemDto.EntityToCartItem());
                    result.Success = true;
                    result.Message = "CartItem Created Successfully";
                }
            }

            result.Data = createCartItemDto;
            return result;
        }

        public async Task<IEnumerable<GetCartItemDto>> GetAllCartItemsAsync()
        {
            var cartItems = await _unitOfWork.CartItems.GetAllEntities()
                                                       .Include(ci => ci.Product)
                                                       .Include(ci => ci.User)
                                                       .ToListAsync();

            return cartItems.Select(ci => ci.EntityToGetCartItemDto());
        }

        public async Task<ServiceResult<UpdateCartItemDto>> UpdateCartItem(UpdateCartItemDto updateCartItemDto)
        {
            var GetCartItem = await GetCartItemByIdAsync(updateCartItemDto.Id);
            var result = new ServiceResult<UpdateCartItemDto>();

            if (!GetCartItem.Success)
            {
                result.Success = false;
                result.Message = "CartItem NotFound!";
            }
            else
            {
                if (updateCartItemDto.Quantity >= 1 && updateCartItemDto.Quantity <= 100)
                {
                    _unitOfWork.CartItems.UpdateEntity(updateCartItemDto.EntityToCartItem());
                    result.Message = "CartItem Updated Successfully";
                    result.Success = true;
                }
                else
                {
                    if(updateCartItemDto.Quantity == 0)
                    {
                        await _unitOfWork.CartItems.DeleteEntityAsync(updateCartItemDto.Id);
                        result.Message = "CartItem Deleted Successfully";
                        result.Success = true;
                    }
                    else
                    {
                        result.Success = false;
                        result.Message = "Quantity must be between 1 and 100";
                    }
                }
            }

            result.Data = updateCartItemDto;
            return result;
        }
    }
}
