using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models
{
    public class Product
    {
        [Key]
        public int ProdID { get; set; }

        [Required]
        public string? Title { get; set; }

        [Required]
        public string? Description { get; set; }

        [Required]
        public string? Author { get; set; }

        [Required]
        public string? ISBN { get; set; }

        [Required]
        [Range(1, 1000)]
        public int ListPrice { get; set; }

        [Required]
        [Range(1, 1000)]
        public int Price { get; set; }

        [Required]
        [Range(1, 1000)]
        public int Price50 { get; set; }

        [Required]
        [Range(1, 1000)]

        public int Price100 { get; set; }

        public int CategoryID { get; set; }
        [ForeignKey("CategoryID")]
        [ValidateNever]
        public Category Category { get; set; }
        [ValidateNever]
        public string ImageURL { get; set; }
        
    }
}
