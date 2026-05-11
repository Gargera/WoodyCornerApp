using System;
using System.Collections.Generic;
using System.Text;
using WoodyCornerApp.BLL.DTOs.CartItemDtos;
using WoodyCornerApp.DAL.Entities;

namespace WoodyCornerApp.BLL.Mapping
{
    public static class CartItemMapping
    {
        public static CartItem EntityToCartItem(this GetCartItemDto getCartItemDto)
        {
            return new CartItem
            {

            };
        }
        public static GetCartItemDto EntityToGetCartItemDto(this CartItem cartItem)
        {
            return new GetCartItemDto
            {

            };
        }
    }
}
