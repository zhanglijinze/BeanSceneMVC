using System.ComponentModel.DataAnnotations;

namespace BeanSceneMVC.Models
{
    public class MenuCategory
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Name { get; set; } = null!;
    }
}
