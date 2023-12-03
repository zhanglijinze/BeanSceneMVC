using BeanSceneMVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BeanSceneMVC.ViewModels
{
    public class ReservationViewModel
    {
        //Reference to the main model
        public  Reservation Reservation { get; set; } = null!;

        //Custom IDs
        public string? SittingId { get; set; }// E.g 2023-11-03:1

        public SelectList TimeslotList { get; set; } = null!;

        public SelectList SittingList { get; set; } = null!;

        /*  public SelectList SittingTypeList { get; set; } = null!;*/
        // Assigned/unassigned rooms
        public SelectList AssignedTablesList { get; set; } = null!;
        public SelectList UnassignedTablesList { get; set; } = null!;

        public List<DateTime> AvailableDates { get; set; } = null!;
    }
}
