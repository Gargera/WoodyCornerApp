using System;
using System.Collections.Generic;
using System.Text;
using WoodyCornerApp.BLL.Common;
using WoodyCornerApp.BLL.DTOs.CartItemDtos;

namespace WoodyCornerApp.BLL.Interfaces
{
    public interface ICartItemService
    {
        public Task<ServiceResult<GetCartItemDto>> GetCartItemByIdAsync(int id);

        public Task<ServiceResult<GetCartItemDto>> DeleteCartItemAsync(int id);

        public Task<ServiceResult<CreateCartItemDto>> CreateCartItemAsync(CreateCartItemDto createCartItemDto);


        public Task<IEnumerable<GetCartItemDto>> GetAllCartItemsAsync();
        
        public Task<ServiceResult<UpdateCartItemDto>> UpdateCartItem(UpdateCartItemDto updateCartItemDto);
    }
}
