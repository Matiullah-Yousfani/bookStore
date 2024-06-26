using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class Category
    {
        [Key]
        public int Ctg_ID { get; set; }
        [Required(ErrorMessage = "The field cannot be Empty")]
        [DisplayName("Category Name")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "The field cannot be Empty")]
        [DisplayName("Dsiplay Order")]
        [Range(minimum: 1, maximum: 100)]
        public int DisplayOrder { get; set; }
    }
}
