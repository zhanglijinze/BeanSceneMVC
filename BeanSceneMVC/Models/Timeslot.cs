using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BeanSceneMVC.Models
{
    public class Timeslot
    {
        
        [Key]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:hh:mm tt}")]
        public DateTime Time { get; set; }

        [NotMapped]
        public string TimeFormatted { get => Time.ToString("hh:mm tt"); }
    }

}
