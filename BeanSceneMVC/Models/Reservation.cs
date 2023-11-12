using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BeanSceneMVC.Models
{
    public class Reservation
    {
        [Key]
        [DisplayName("Reservation No")]
        public int Id { get; set; }
        [StringLength(450)]
        [DisplayName("User")]
        public string? UserId { get; set; }

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
        [DisplayName("Area")]
        public int AreaId { get; set; }
        [Required]
        [DisplayName("Number of People")]
        [Range(1, 5000)]
        public ushort NumberOfPeople { get; set; } = 1;
        [Required]
        [DisplayName("First Name")]
        [StringLength(50, MinimumLength = 2)]
        public string FirstName { get; set; } = null!;
        [Required]
        [DisplayName("Last Name")]
        [StringLength(50, MinimumLength = 2)]
        public string LastName { get; set; } = null!;

        [NotMapped]
        [DisplayName("Full Name")]
        public string FullName { get => $"{FirstName.Trim()} {LastName}"; }

        [DisplayName("Email")]
        [StringLength(256)]
        [EmailAddress]
        public string? Email { get; set; }

        [DisplayName("Phone")]
        [StringLength(20)]
        public string? Phone { get; set; }

        [DisplayName("Note")]
        public string? Note { get; set; }

        [Required]
        public StatusEnum Status { get; set; }

        // Define enum for statuses 
        public enum StatusEnum
        {
            Pending = 0,
            Confirmed = 1,
            InProgress = 2,
            Completed = 3,
            Cancelled = 4
        }

        [Required]
        public SourceEnum Source { get; set; }

        // Define enum for statuses 
        public enum SourceEnum
        {
            Web = 0,
            [Display(Name ="In Person")]
            In_Person = 1,
            Email = 2,
            Phone = 3,
           
        }


        //Association (Navigation properties)
        [Required]
        [DisplayName("Sitting")]
        [ForeignKey("Date,SittingTypeId")]
        // Associations (Navigation properties)
        public Sitting Sitting { get; set; } = null!;
        [Required]
        [DisplayName("Start Time")]
        public Timeslot StartTime { get; set; } = null!;
        [Required]
        [DisplayName("End Time")]
        public Timeslot EndTime { get; set; } = null!;



        [Required]
        public Area Area { get; set; } = null!;

        //Link to User entity ... need to ustomise the Identity User model.....
        [ForeignKey("UserId")]
        [DisplayName("User")]
        public ApplicationUser? User { get; set; }

        //Define a many-to-many relationship between reservation and table (check the table model)
        [DisplayName("Assigned Tables")]
        public List<Table> Tables { get; } = new();
    }
}
