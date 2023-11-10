using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BeanSceneMVC.Models
{
    [Index(nameof(Name),IsUnique =true)]
    public class Area
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50,MinimumLength =2, ErrorMessage = "Must be between 2-50 characters")]
        public string Name { get; set; }

        public Area() { Name = ""; }

        public Area(string name) 
        {
            Name = name;        
        }

    }
}
