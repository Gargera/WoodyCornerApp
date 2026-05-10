using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WoodyCornerApp.DAL.Entities
{
    public class Room
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        [Display(Name = "Room Type")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(50, MinimumLength = 10)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Image")]
        public string ImagePath { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
