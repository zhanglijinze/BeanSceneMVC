﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BeanSceneMVC.Data;
using BeanSceneMVC.Models;
using BeanSceneMVC.ViewModels;
using NuGet.Configuration;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace BeanSceneMVC.Controllers
{
    [Authorize(Roles = "User,Staff,Manager")]
    public class ReservationsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public ReservationsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Reservations
        public async Task<IActionResult> Index()
        {
            /*var applicationDbContext = _context.Reservations.Include(r => r.EndTime).Include(r => r.Sitting).Include(r => r.StartTime).Include(r => r.User);
            return View(await applicationDbContext.ToListAsync());*/
            IQueryable<Reservation> reservations;
            // Get currently logged-in user
            ApplicationUser user = await GetLoggedInUserAsync();
            // Check if user is logged in and has the "User" role
            if (user.IsUser)
            {
                // Filter the reservations to ONLY show the user's own reservations
                reservations = _context.Reservations
                    .Where(r => r.UserId == user.Id)
                    .Include(r => r.StartTime)
                    .Include(r => r.EndTime)
                    .Include(r => r.Sitting)
                    .Include(r => r.Sitting.SittingType)
                    .Include(r => r.User);
            }
            else
            {
                reservations = _context.Reservations
                    .Include(r => r.StartTime)
                    .Include(r => r.EndTime)
                    .Include(r => r.Sitting)
                    .Include(r => r.Sitting.SittingType)
                    .Include(r => r.User);
            }
            // Load the view
            return View(await reservations.ToListAsync());
        }
        // GET: Reservations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Reservations == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .Include(r => r.EndTime)
                .Include(r => r.Sitting)
                .Include(r => r.StartTime)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            // Access control
            // Get currently logged-in user
            ApplicationUser user = await GetLoggedInUserAsync();
            // Check if logged in as "User" and if they "own" the reservation
            if (user.IsUser && reservation.UserId != user.Id)
            {
                // Throw error
                return BadRequest("You do not have permission to access this reservation! 🔐");
            }


            return View(reservation);
        }

        // GET: Reservations/Create
        [AllowAnonymous]
        public async Task<IActionResult> Create()
        {
            /*    ViewData["EndTimeId"] = new SelectList(_context.Timeslots, "Time", "Time");
                ViewData["Date"] = new SelectList(_context.Sittings, "Date", "Date");
                ViewData["StartTimeId"] = new SelectList(_context.Timeslots, "Time", "Time");
                ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");*/
            //Load view with the view model
            // View model
            ReservationViewModel viewModel = GenerateDefaultViewModel();
            // Get currently logged-in user
            ApplicationUser user = await GetLoggedInUserAsync();
            // Check if user is logged in and has the "User" role
            if (user.IsUser)
            {
                // Copy user's contact info into the reservation (to show in the form)
                viewModel.Reservation.FirstName = user.FirstName;
                viewModel.Reservation.LastName = user.LastName;
                viewModel.Reservation.Email = user.Email;
                viewModel.Reservation.Phone = user.PhoneNumber;
            }
            // Load view with the view model
            return View(viewModel);
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        /*  public async Task<IActionResult> Create([Bind("Id,UserId,Date,SittingTypeId,StartTimeId,EndTimeId,NumberOfPeople,FirstName,LastName,Email,Phone,Note,Status,Source")] Reservation reservation)*/
        public async Task<IActionResult> Create(ReservationViewModel viewModel)
        {
            //Sitting ID regex pattern for validation (Data:Type, e.g 2023-11-28:1)

            Regex regexSittingId = new Regex(@"^(\d{4}-\d{2}-\d{2}):(\d)$");

            // Match sitting ID against regex pattern
            Match match = regexSittingId.Match(viewModel.SittingId ?? "");

            // Check if sitting ID is not valid
            if (!match.Success) return BadRequest("Invalid Sitting ID, should be in the format: 2023-11-03:1");

            // Extract the sitting date and sitting type ID
            string sittingDateString = match.Groups[1].Value;
            int sittingTypeId = int.Parse(match.Groups[2].Value);

            // Convert date string into DateTime
            DateTime sittingDate;
            if (!DateTime.TryParse(sittingDateString, out sittingDate)) return BadRequest("Invalid sitting date, must be 'yyyy-mm-dd'.");

            // Check SittingType exists
            SittingType? sittingType = await _context.SittingTypes.FindAsync(sittingTypeId);
            if (sittingType == null) return NotFound("Sitting Type not found.");

            // Check Sitting exists
            var sitting = await _context.Sittings
                .Include(s => s.SittingType)
                .Include(s => s.StartTime)
                .Include(s => s.EndTime)
                .FirstOrDefaultAsync(s => s.SittingTypeId == sittingTypeId && s.Date == sittingDate);
            if (sitting == null) return NotFound("Sitting not found.");


            //Get model from the view model
            Reservation reservation = viewModel.Reservation;

            // Attach session data to the reservation
            reservation.Sitting = sitting;


            //Find related entities based on their foreign key values (IDs)


            Timeslot? startTime = await _context.Timeslots.FindAsync(reservation.StartTimeId);

            Timeslot? endTime = await _context.Timeslots.FindAsync(reservation.EndTimeId);

            //Check if related entities don't exist - throw 404 or a nice error message...

           
            if (startTime == null) return NotFound("Start timeslot not found");
            if (endTime == null) return NotFound("End timeslot not found");

            //Assign related entities to the model
         
            reservation.StartTime = startTime;
            reservation.EndTime = endTime;

            //Manually revalidate the model
            ModelState.Clear();
            TryValidateModel(reservation);

            //Get currently logged-in user and assign...

            ApplicationUser user = await GetLoggedInUserAsync();
            // Check if user is logged in and has the "User" role
            if (user.IsUser)
            {
                // Assign the user to the reservation
                reservation.UserId = user.Id;
                reservation.User = user;
            }


         

            /*
           * Validate start & end times (use custom ModelState validation)
           */

            //validate start & end times

            if (!reservation.IsValidDuration())
            {
                ModelState.AddModelError("Reservation.EndTimeId", $"Booking must be {reservation.MinBookingLengthMinutes}-{reservation.MaxBookingLengthMinutes} minutes long.");
            }
            if (!reservation.IsValidStartTime()) ModelState.AddModelError("Reservation.StartTimeId", "Must be within the session.");
            if (!reservation.IsValidEndTime()) ModelState.AddModelError("Reservation.EndTimeId", "Must be within the session.");

            //validate capacity

            if (!reservation.IsValidCapacity())
            {
                ModelState.AddModelError("Reservation.NumberOfPeople", $"Exceeding capacity {sitting.Capacity}");

            }

            //Set the reservation status to "Pending"

            reservation.Status = Reservation.StatusEnum.Pending;

            //Check model is valid
            if (ModelState.IsValid)
            {
                _context.Add(reservation);
                await _context.SaveChangesAsync();

                //Redirect anonymous user (or customer) to "confirmation" view

                // Check if user is anonymous or "User"/customer
                if (user.UserName == null || user.IsUser)
                {
                    // Redirect anonymous user (or customer) to "confirmation" view
                    return View("CreateConfirmed", reservation);
                }
                else
                {
                    // Staff/manager - redirect to reservation listing

                    return RedirectToAction(nameof(Index));
                }
                
            }
            /* ViewData["EndTimeId"] = new SelectList(_context.Timeslots, "Time", "Time", reservation.EndTimeId);
             ViewData["Date"] = new SelectList(_context.Sittings, "Date", "Date", reservation.Date);
             ViewData["StartTimeId"] = new SelectList(_context.Timeslots, "Time", "Time", reservation.StartTimeId);
             ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", reservation.UserId);*/

            //Build the view model (viewModel as the parameter will take the viewModel that has the reservation and pass it in here and method would not create a new viewModel with reservation but populate with dropdown list. )
             viewModel = GenerateDefaultViewModel(viewModel);

            //Put sitting data into the view model
            viewModel.Reservation = reservation;
            //Load the view with our View Model
            return View(viewModel);
        }

        /*   // GET: Reservations/Edit/5
           public async Task<IActionResult> Edit(int? id)
           {
               if (id == null || _context.Reservations == null)
               {
                   return NotFound();
               }

               var reservation = await _context.Reservations.FindAsync(id);
               if (reservation == null)
               {
                   return NotFound();
               }
               ViewData["EndTimeId"] = new SelectList(_context.Timeslots, "Time", "Time", reservation.EndTimeId);
               ViewData["Date"] = new SelectList(_context.Sittings, "Date", "Date", reservation.Date);
               ViewData["StartTimeId"] = new SelectList(_context.Timeslots, "Time", "Time", reservation.StartTimeId);
               ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", reservation.UserId);
               return View(reservation);
           }*/

        // GET: Reservations/Edit/5
        [Authorize(Roles = "Staff,Manager")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Reservations == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }
            /*ViewData["EndTimeId"] = new SelectList(_context.Timeslots, "Time", "Time", reservation.EndTimeId);
            ViewData["Date"] = new SelectList(_context.Sittings, "Date", "Date", reservation.Date);
            ViewData["StartTimeId"] = new SelectList(_context.Timeslots, "Time", "Time", reservation.StartTimeId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", reservation.UserId);
            return View(reservation);*/
            // Build the view model
            ReservationViewModel viewModel = GenerateDefaultViewModel();

            // Put reservation data into the view model
            viewModel.Reservation = reservation;
            viewModel.SittingId = $"{reservation.Date.ToString("yyyy-MM-dd")}:{reservation.SittingTypeId}";
            // Load the view with our View Model
            return View(viewModel);

        }

  /*      // POST: Reservations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,Date,SittingTypeId,StartTimeId,EndTimeId,NumberOfPeople,FirstName,LastName,Email,Phone,Note,Status,Source")] Reservation reservation)
        {
            if (id != reservation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationExists(reservation.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["EndTimeId"] = new SelectList(_context.Timeslots, "Time", "Time", reservation.EndTimeId);
            ViewData["Date"] = new SelectList(_context.Sittings, "Date", "Date", reservation.Date);
            ViewData["StartTimeId"] = new SelectList(_context.Timeslots, "Time", "Time", reservation.StartTimeId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", reservation.UserId);
            return View(reservation);
        }*/

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Staff,Manager")]
        public async Task<IActionResult> Edit(int id, ReservationViewModel viewModelEdit)
        {
            //Get model from the view model
            Reservation reservation = viewModelEdit.Reservation;

            if (id != reservation.Id)
            {
                return NotFound();
            }

            //Sitting ID regex pattern for validation (Data:Type, e.g 2023-11-28:1)

            Regex regexSittingId = new Regex(@"^(\d{4}-\d{2}-\d{2}):(\d)$");

            // Match sitting ID against regex pattern
            Match match = regexSittingId.Match(viewModelEdit.SittingId ?? "");

            // Check if sitting ID is not valid
            if (!match.Success) return BadRequest("Invalid Sitting ID, should be in the format: 2023-11-03:1");

            // Extract the sitting date and sitting type ID
            string sittingDateString = match.Groups[1].Value;
            int sittingTypeId = int.Parse(match.Groups[2].Value);

            // Convert date string into DateTime
            DateTime sittingDate;
            if (!DateTime.TryParse(sittingDateString, out sittingDate)) return BadRequest("Invalid sitting date, must be 'yyyy-mm-dd'.");

            // Check SittingType exists
            SittingType? sittingType = await _context.SittingTypes.FindAsync(sittingTypeId);
            if (sittingType == null) return NotFound("Sitting Type not found.");

            // Check Sitting exists
            var sitting = await _context.Sittings
                .Include(s => s.SittingType)
                .Include(s => s.StartTime)
                .Include(s => s.EndTime)
                .FirstOrDefaultAsync(s => s.SittingTypeId == sittingTypeId && s.Date == sittingDate);
            if (sitting == null) return NotFound("Sitting not found.");


            // Attach session data to the reservation
            reservation.Sitting = sitting;




            //Find related entities based on their foreign key values (IDs)


            Timeslot? startTime = await _context.Timeslots.FindAsync(reservation.StartTimeId);

            Timeslot? endTime = await _context.Timeslots.FindAsync(reservation.EndTimeId);

            //Check if related entities don't exist - throw 404 or a nice error message...


            if (startTime == null) return NotFound("Start timeslot not found");
            if (endTime == null) return NotFound("End timeslot not found");

            //Assign related entities to the model

            reservation.StartTime = startTime;
            reservation.EndTime = endTime;


            //Manually revalidate the model
            ModelState.Clear();
            TryValidateModel(reservation);


            //validate start & end times

            if (!reservation.IsValidDuration())
            {
                ModelState.AddModelError("Reservation.EndTimeId", $"Booking must be {reservation.MinBookingLengthMinutes}-{reservation.MaxBookingLengthMinutes} minutes long.");
            }
            if (!reservation.IsValidStartTime()) ModelState.AddModelError("Reservation.StartTimeId", "Must be within the session.");
            if (!reservation.IsValidEndTime()) ModelState.AddModelError("Reservation.EndTimeId", "Must be within the session.");


            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationExists(reservation.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            /* ViewData["EndTimeId"] = new SelectList(_context.Timeslots, "Time", "Time", reservation.EndTimeId);
             ViewData["Date"] = new SelectList(_context.Sittings, "Date", "Date", reservation.Date);
             ViewData["StartTimeId"] = new SelectList(_context.Timeslots, "Time", "Time", reservation.StartTimeId);
             ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", reservation.UserId);
             return View(reservation);*/
            ReservationViewModel viewModel = GenerateDefaultViewModel();

            //Put session data into the view model
            viewModel.Reservation = reservation;
            //Load the view with our View Model
            return View(viewModel);
        }


        // GET: Reservations/Delete/5
        [Authorize(Roles = "Staff,Manager")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Reservations == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .Include(r => r.EndTime)
                .Include(r => r.Sitting)
                .Include(r => r.StartTime)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // POST: Reservations/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Staff,Manager")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Reservations == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Reservations'  is null.");
            }
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation != null)
            {
                _context.Reservations.Remove(reservation);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Reservations/Edit/5/Confirmed
        [HttpGet("Reservations/Edit/{id}/{status}")]
        [Authorize(Roles = "Staff,Manager")]
        public async Task<IActionResult> Edit(int id, string status)
        {
            // Find a reservation by ID
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null) return NotFound("Reservation not found.");
            // Validate status
            if (!Enum.TryParse<Reservation.StatusEnum>(status, out Reservation.StatusEnum statusEnum))
                return BadRequest("Status not valid.");
            // Change the status
            reservation.Status = statusEnum;
            // Update the database
            _context.Update(reservation);
            await _context.SaveChangesAsync();
            // Redirect to Details view
            return RedirectToAction(nameof(Details), new { id = reservation.Id });
        }

        private bool ReservationExists(int id)
        {
          return (_context.Reservations?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private ReservationViewModel GenerateDefaultViewModel(ReservationViewModel?viewModel=null)
        {
            //Check if no view model passed in
            if (viewModel == null)
            {
                // New view model with empty reservation
                viewModel = new ReservationViewModel();

                //Set reservation to null (if no existing reservation)
                viewModel.Reservation = new Reservation();
            }

            //Populate the selest list items
            /* viewModel.SittingTypeList = new SelectList(_context.SittingTypes, "Id", "Name");*/
            viewModel.TimeslotList = new SelectList(_context.Timeslots.ToList(), "Time", "TimeFormatted");

            viewModel.SittingList = new SelectList(
                _context.Sittings.Where(s=>s.Date>=DateTime.Today&&s.Status==0)
                .Select(s => new SelectListItem
                {
                    //E.g. 2023-10-26:1
                    Value = s.Date.ToString("yyyy-MM-dd") + ":" +
                s.SittingTypeId,
                    //E.g. 26/10/2023 - Morning (09:00 AM-11:00 AM)
                    Text = $"{s.Date.ToShortDateString()} - {s.SittingType.Name} ({s.StartTime.TimeFormatted}-{s.EndTime.TimeFormatted})"

                })
                .ToList(),
                "Value",
                "Text"
               );
     
            return viewModel;
        }

            private async Task<ApplicationUser> GetLoggedInUserAsync()
            {
                // Get the currently logged-in user (and pass through the UserManager context)
                ApplicationUser user = await _userManager.GetUserAsync(this.User) ?? new ApplicationUser();
                user.UserManager = _userManager;
                return user;
            }
        }
}
