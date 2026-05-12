using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WoodyCornerApp.DAL.Entities;

namespace WoodyCornerApp.BLL.DTOs.CartItemDtos
{
    public class UpdateCartItemDto
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public string UserId { get; set; } = null!;

        [Required]
        [Range(1, 1000)]
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }
    }
}
