using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BeanSceneMVC.Models
{
    public class Table
    {
        [Key]
        [StringLength(5, MinimumLength = 2)]
        [RegularExpression(@"^[A-Z]{1,2}\d{1,3}$", ErrorMessage = "Must follow the pattern XX000, e.g. M1, AB001")]
        public string Code { get; set; }
        [Required]
        [DisplayName("Area ID")]
        public int AreaId { get; set; }
        public Area? Area { get; set; }

        public Table() {
            Code = "";
        }
        public Table(string code, int areaId) 
        { 
            Code= code;
            AreaId = areaId;
        }
    }
}
