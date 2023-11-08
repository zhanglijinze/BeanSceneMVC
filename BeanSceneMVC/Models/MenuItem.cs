using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeanSceneMVC.Models
{
    [Index(nameof(Name),IsUnique =true)]
    public class MenuItem
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MaxLength(500)]
        [DisplayFormat(HtmlEncode =false)]
        public string Description { get; set; }
        [Required]
        [RegularExpression(@"^\$?\d+(?:\.\d{1,2})?$", ErrorMessage = "Must be a number with up to 2 decimal places")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
        [Required]
        [DataType(DataType.ImageUrl)]
        public string Photo { get; set; }
        [Required]
        [DisplayName("Gluten-Free")]
        public bool IsGlutenFree { get; set; }
        [Required]
        [DisplayName("Diary-Free")]
        public bool IsDiaryFree { get; set; }
        [Required]
        [DisplayName("Vegetarian")]
        public bool IsVegetarian { get; set; }
        [Required]
        [DisplayName("Vegan")]
      public bool IsVegan { get; set; }
        [Required]
        [DisplayName("Allergen-Free")]
      public bool IsAllergenFree { get; set; }
        [Required]
      public int MenuCategoryId { get; set; }
        [DisplayName("Menu Category")]
        public MenuCategory MenuCategory { get; set; }
    }
}
