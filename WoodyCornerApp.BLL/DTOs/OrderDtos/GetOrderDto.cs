using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WoodyCornerApp.DAL.Entities;
using WoodyCornerApp.BLL.DTOs.OrderItemDtos;

namespace WoodyCornerApp.BLL.DTOs.OrderDtos
{
    public class GetOrderDto
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        [Display(Name = "Order Date")]
        public DateTime OrderDate { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        [Column(TypeName = "decimal(10,2)")]
        [Display(Name = "Total Price")]
        public decimal TotalPrice { get; set; }

        [EnumDataType(typeof(OrderStatus))]
        [Display(Name = "Order Status")]
        public OrderStatus OrderStatus { get; set; }

        [Required]
        [StringLength(300, MinimumLength = 10)]
        [Display(Name = "Shipping Address")]
        public string ShippingAddress { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        [Display(Name = "Shipping City")]
        public string ShippingCity { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }

        public ICollection<GetOrderItemDto> OrderItems { get; set; }
    }
}
