using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WoodyCornerApp.BLL.DTOs.CartItemDtos
{
    public class CreateCartItemDto
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        [Range(1, 100)]
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }
    }
}
