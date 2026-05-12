using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WoodyCornerApp.DAL.Entities;
using WoodyCornerApp.BLL.DTOs.ProductDtos;

namespace WoodyCornerApp.BLL.DTOs.CartItemDtos
{
    public class GetCartItemDto
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        [Range(1, 1000)]
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
    }
}
