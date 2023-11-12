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
        [DisplayName("Area")]
        public int AreaId { get; set; }
        public Area? Area { get; set; }

        //can also implement entity relationship in DBContext but it's not encouraged. OnModelCreating method. 

        //Define a many-to-many relationship between reservation and room (check the room model)
        [DisplayName("Reservations")]
        public List<Reservation> Reservations { get; } = new();

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
