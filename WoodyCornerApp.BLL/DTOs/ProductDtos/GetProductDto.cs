using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WoodyCornerApp.DAL.Entities;
using WoodyCornerApp.BLL.DTOs.CartItemDtos;
using WoodyCornerApp.BLL.DTOs.OrderItemDtos;

namespace WoodyCornerApp.BLL.DTOs.ProductDtos
{
    public class GetProductDto
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 2)]
        [Display(Name = "Product Name")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(1000, MinimumLength = 10)]
        [Display(Name = "Description")]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        [Display(Name = "Price")]
        public decimal Price { get; set; }

        [Required]
        [Range(0, 1000000)]
        [Display(Name = "Stock Quantity")]
        public int StockQuantity { get; set; }

        [Required]
        [Display(Name = "Image")]
        public string ImagePath { get; set; }

        [Required]
        [Display(Name = "Room")]
        public int RoomId { get; set; }

        [ForeignKey("RoomId")]
        public Room Room { get; set; } = null!;

        public ICollection<GetCartItemDto> CartItems { get; set; } = new List<GetCartItemDto>();
        public ICollection<GetOrderItemDto> OrderItems { get; set; } = new List<GetOrderItemDto>();
    }
}
