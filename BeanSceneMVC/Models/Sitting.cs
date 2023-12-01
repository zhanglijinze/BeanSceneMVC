using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeanSceneMVC.Models
{
    [PrimaryKey(nameof(Date), nameof(SittingTypeId))]
    public class Sitting
    {
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime Date { get; set; }
        [Required]
        [DisplayName("Sitting Type")]
        public int SittingTypeId { get; set; }
        [Required]
        [DisplayName("Start Time")]
        public DateTime StartTimeId { get; set; }
        [Required]
        [DisplayName("End Time")]
        public DateTime EndTimeId { get; set; }
        [Required]

        public StatusEnum Status { get; set; }

        // Define enum for statuses 
        public enum StatusEnum
        {
            Available = 0,
            Unavailable = 1,

        }
        [Required]
        [Range(1,100)]
        public int Capacity { get; set; }

        [Required]
        [DisplayName("Sitting Type")]
        // Associations (Navigation properties)
        public SittingType SittingType { get; set; } = null!;
        [Required]
        [DisplayName("Start Time")]
        public Timeslot StartTime { get; set; } = null!;
        [Required]
        [DisplayName("End Time")]
        public Timeslot EndTime { get; set; } = null!;

        [NotMapped]
        public int MinSittingLengthMinutes = 60;
        [NotMapped]
        public int MaxSittingLengthMinutes = 360;

        public bool IsValidDuration() 
        {
            TimeSpan timeSpan = EndTimeId - StartTimeId;
            return (EndTimeId>StartTimeId)&&(timeSpan.TotalMinutes>=60&&timeSpan.TotalMinutes<=360) ;
        }
    }
}
