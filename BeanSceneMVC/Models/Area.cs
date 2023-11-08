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
        [StringLength(5,MinimumLength =2)]
        public string Name { get; set; }

        public Area() { Name = ""; }

        public Area(string name) 
        {
            Name = name;        
        }

    }
}
