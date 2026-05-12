using System;
using System.Collections.Generic;
using System.Text;
using WoodyCornerApp.BLL.DTOs.CartItemDtos;
using WoodyCornerApp.DAL.Entities;

namespace WoodyCornerApp.BLL.Mapping
{
    public static class CartItemMapping
    {
        public static GetCartItemDto EntityToGetCartItemDto(this CartItem cartItem)
        {
            return new GetCartItemDto
            {
                Id = cartItem.Id,
                ProductId = cartItem.ProductId,
                Quantity = cartItem.Quantity,
                UserId = cartItem.UserId,
                Product = cartItem.Product,
                User = cartItem.User
            };
        }

        public static CartItem EntityToCartItem(this UpdateCartItemDto updateCartItemDto)
        {
            return new CartItem
            {
                Id = updateCartItemDto.Id,
                ProductId = updateCartItemDto.ProductId,
                Quantity = updateCartItemDto.Quantity,
                UserId = updateCartItemDto.UserId
            };
        }

        public static CartItem EntityToCartItem(this CreateCartItemDto createCartItemDto)
        {
            return new CartItem
            {
                ProductId = createCartItemDto.ProductId,
                Quantity = createCartItemDto.Quantity,
                UserId = createCartItemDto.UserId
            };
        }
    }
}
