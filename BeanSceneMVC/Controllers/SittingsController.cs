using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BeanSceneMVC.Data;
using BeanSceneMVC.Models;
using BeanSceneMVC.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace BeanSceneMVC.Controllers
{
    [Authorize(Roles ="Manager")]
    public class SittingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SittingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Sittings
        /*[AllowAnonymous] // Allow access by anyone like make reservation*/
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Sittings.Include(s => s.EndTime).Include(s => s.SittingType).Include(s => s.StartTime);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Sittings/Details/5/yyyy-mm-dd
        [HttpGet("Sittings/Details/{sittingTypeId}/{dateString}")]
        public async Task<IActionResult> Details(int sittingTypeId, string dateString)
        {
            /*  if (id == null || _context.Sittings == null)
              {
                  return NotFound();
              }*/
            //Convert date string to DateTime
            DateTime sittingDate;
            if (!DateTime.TryParse(dateString, out sittingDate)) return BadRequest("Invalid sitting date, must be'yyyy-mm-dd'. ");

            //Check SectionType exists
            SittingType? sittingType = await _context.SittingTypes.FindAsync(sittingTypeId);
            if (sittingType == null) return NotFound("Sitting Type not found");

            var sitting = await _context.Sittings
                .Include(s => s.EndTime)
                .Include(s => s.SittingType)
                .Include(s => s.StartTime)
                .FirstOrDefaultAsync(m => m.SittingTypeId == sittingTypeId&&m.Date==sittingDate);
            if (sitting == null)
            {
                return NotFound("Sitting not found");
            }

            return View(sitting);
        }



        // GET: Sittings/Create
        //Allow access to Staff or user
        //[Authorize(Roles="Staff")]
        //Allow access to a user that has both Staff And user roles
        /*  [Authorize(Roles ="Staff")]
          [Authorize(Roles ="User")]*/
        public IActionResult Create()
        {
          /*  ViewData["EndTimeId"] = new SelectList(_context.Timeslots, "Time", "Time");
            ViewData["SittingTypeId"] = new SelectList(_context.SittingTypes, "Id", "Name");
            ViewData["StartTimeId"] = new SelectList(_context.Timeslots, "Time", "Time");
            return View();*/
            return View( GenerateDefaultViewModel());
        }

        // POST: Sittings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Date,SittingTypeId,StartTimeId,EndTimeId,Status,Capacity")] Sitting sitting)
        {
            
            /*ViewData["EndTimeId"] = new SelectList(_context.Timeslots, "Time", "Time", sitting.EndTimeId);
            ViewData["SittingTypeId"] = new SelectList(_context.SittingTypes, "Id", "Name", sitting.SittingTypeId);
            ViewData["StartTimeId"] = new SelectList(_context.Timeslots, "Time", "Time", sitting.StartTimeId);*/

            //Find related entities based on their foreign key values (IDs)
            SittingType? sittingType = await _context.SittingTypes.FindAsync(sitting.SittingTypeId);

            Timeslot? startTime = await _context.Timeslots.FindAsync(sitting.StartTimeId);

            Timeslot? endTime = await _context.Timeslots.FindAsync(sitting.EndTimeId);

            //Check if related entities don't exist - throw 404 or a nice error message...

            if (sittingType == null) return NotFound("Sitting type not found");
            if (startTime == null) return NotFound("Start timeslot not found");
            if (endTime == null) return NotFound("End timeslot not found");

            //Assign related entities to the model
            sitting.SittingType = sittingType;
            sitting.StartTime = startTime;
            sitting.EndTime = endTime;

            //Manually revalidate the model
            ModelState.Clear();
            TryValidateModel(sitting);

            //check for conflicting sitting (primary key already exists)

            if (SittingExists(sitting.Date, sitting.SittingTypeId))
            {
                ModelState.AddModelError("Sitting.Date", "Sitting already exists (same date and sitting type) ");
            }

            if (!sitting.IsValidDuration())
            {
                ModelState.AddModelError("Sitting.EndTimeId", $"Start time must be less than end time and sitting must be {sitting.MinSittingLengthMinutes}-{sitting.MaxSittingLengthMinutes} minutes long.");
            }


            // check if model is valid

            if (ModelState.IsValid)
            {
                _context.Add(sitting);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            


            //Build the view model
            SittingViewModel viewModel = GenerateDefaultViewModel();

            //Put sitting data into the view model
            viewModel.Sitting = sitting;
            //Load the view with our View Model
            return View(viewModel);
        }

        // GET: Sittings/Edit/5/yyyy-mm-dd
        [HttpGet("Sittings/Edit/{sittingTypeId}/{dateString}")]
        public async Task<IActionResult> Edit(int sittingTypeId, string dateString)
        {
            /* if (id == null || _context.Sittings == null)
             {
                 return NotFound();
             }

             var sitting = await _context.Sittings.FindAsync(id);
             if (sitting == null)
             {
                 return NotFound();
             }*/
            //Convert date string to DateTime
            DateTime sittingDate;
            if (!DateTime.TryParse(dateString, out sittingDate)) return BadRequest("Invalid sitting date, must be'yyyy-mm-dd'. ");

            //Check SectionType exists
            SittingType? sittingType = await _context.SittingTypes.FindAsync(sittingTypeId);
            if (sittingType == null) return NotFound("Sitting Type not found");

            var sitting = await _context.Sittings
                .FindAsync(sittingDate, sittingTypeId);
               /* .FirstOrDefaultAsync(m => m.SittingTypeId == sittingTypeId && m.Date == sittingDate);*/
            if (sitting == null)
            {
                return NotFound("Sitting not found");
            }

       
            //Build the view model
            SittingViewModel viewModel = GenerateDefaultViewModel();

            //Put sitting data into the view model
            viewModel.Sitting = sitting;
            //Load the view with our View Model
            return View(viewModel);
        }

        // POST: Sittings/Edit/5/yyyy-mm-dd
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Sittings/Edit/{sittingTypeId}/{dateString}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int sittingTypeId,string dateString, [Bind("Date,SittingTypeId,StartTimeId,EndTimeId,Status,Capacity")] Sitting sitting)
        {
            /* if (id != sitting.Date)
             {
                 return NotFound();
             }*/

            //Convert date string to DateTime
            DateTime sittingDate;
            if (!DateTime.TryParse(dateString, out sittingDate)) return BadRequest("Invalid sitting date, must be'yyyy-mm-dd'. ");

            //Check SectionType exists
            SittingType? sittingType = await _context.SittingTypes.FindAsync(sittingTypeId);
            if (sittingType == null) return NotFound("Sitting Type not found");

            //check Sitting exists
            //find.. method can only be used with collection like Sittings here but not with other entities involved (associated) and only work on key fields.

            if (!SittingExists(sittingDate, sittingTypeId))
            {
                return NotFound("Sitting not found");
            }

            //Find related entities based on their foreign key values (IDs)
            //SittingType? sittingType = await _context.SittingTypes.FindAsync(sitting.SittingTypeId);

            Timeslot? startTime = await _context.Timeslots.FindAsync(sitting.StartTimeId);

            Timeslot? endTime = await _context.Timeslots.FindAsync(sitting.EndTimeId);

            //Check if related entities don't exist - throw 404 or a nice error message...

            if (sittingType == null) return NotFound("Sitting type not found");
            if (startTime == null) return NotFound("Start timeslot not found");
            if (endTime == null) return NotFound("End timeslot not found");

            //Assign related entities to the model
            sitting.SittingType = sittingType;
            sitting.StartTime = startTime;
            sitting.EndTime = endTime;

            //Manually revalidate the model
            ModelState.Clear();
            TryValidateModel(sitting);

            if (!sitting.IsValidDuration())
            {
                ModelState.AddModelError("Sitting.EndTimeId", $"Start time must be less than end time and sitting must be {sitting.MinSittingLengthMinutes}-{sitting.MaxSittingLengthMinutes} minutes long.");
            }



            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sitting);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SittingExists(sitting.Date,sitting.SittingTypeId))
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
            /* ViewData["EndTimeId"] = new SelectList(_context.Timeslots, "Time", "Time", sitting.EndTimeId);
             ViewData["SittingTypeId"] = new SelectList(_context.SittingTypes, "Id", "Name", sitting.SittingTypeId);
             ViewData["StartTimeId"] = new SelectList(_context.Timeslots, "Time", "Time", sitting.StartTimeId);*/
            SittingViewModel viewModel = GenerateDefaultViewModel();
            viewModel.Sitting = sitting;
            return View(viewModel);
        }

        // GET: Sittings/Delete/1/yyyy-mm-dd
        [Authorize(Roles = "Manager")]
        [HttpGet("Sittings/Delete/{sittingTypeId}/{dateString}")]
        public async Task<IActionResult> Delete(int sittingTypeId, string dateString)
        {
            /*  if (id == null || _context.Sittings == null)
              {
                  return NotFound();
              }

              var sitting = await _context.Sittings
                  .Include(s => s.EndTime)
                  .Include(s => s.SittingType)
                  .Include(s => s.StartTime)
                  .FirstOrDefaultAsync(m => m.Date == id);
              if (sitting == null)
              {
                  return NotFound();
              }*/
            //Convert date string to DateTime
            DateTime sittingDate;
            if (!DateTime.TryParse(dateString, out sittingDate)) return BadRequest("Invalid sitting date, must be'yyyy-mm-dd'. ");

            //Check SectionType exists
            SittingType? sittingType = await _context.SittingTypes.FindAsync(sittingTypeId);
            if (sittingType == null) return NotFound("Sitting Type not found");

            var sitting = await _context.Sittings
                .Include(s => s.EndTime)
                .Include(s => s.SittingType)
                .Include(s => s.StartTime)
                .FirstOrDefaultAsync(m => m.SittingTypeId == sittingTypeId && m.Date == sittingDate);
            if (sitting == null)
            {
                return NotFound("Sitting not found");
            }

            return View(sitting);
        }

        // POST: Sittings/Delete/1/yyyy-mm-dd
        [Authorize(Roles = "Manager")]
        [HttpPost("Sittings/Delete/{sittingTypeId}/{dateString}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int sittingTypeId, string dateString)
        {
            /* if (_context.Sittings == null)
             {
                 return Problem("Entity set 'ApplicationDbContext.Sittings'  is null.");
             }
             var sitting = await _context.Sittings.FindAsync(id);
             if (sitting != null)
             {
                 _context.Sittings.Remove(sitting);
             }

             await _context.SaveChangesAsync();
             return RedirectToAction(nameof(Index));*/
            //Convert date string to DateTime
            DateTime sittingDate;
            if (!DateTime.TryParse(dateString, out sittingDate)) return BadRequest("Invalid sitting date, must be'yyyy-mm-dd'. ");

            //Check SectionType exists
            SittingType? sittingType = await _context.SittingTypes.FindAsync(sittingTypeId);
            if (sittingType == null) return NotFound("Sitting Type not found");

            var sitting = await _context.Sittings
                .FindAsync(sittingDate, sittingTypeId);
            /* .FirstOrDefaultAsync(m => m.SittingTypeId == sittingTypeId && m.Date == sittingDate);*/
            if (sitting == null)
            {
                return NotFound("Sitting not found");
            }
            

            var reservationGroup = 
                _context.Reservations.Include(r =>r.Sitting).Where(s=> s.SittingTypeId== sittingTypeId && s.Date == sittingDate).ToList();
            if(reservationGroup.Count > 0)
            {
                return NotFound("Sitting was linked to at least one reservation. Delete failed");
            }
            _context.Sittings.Remove(sitting);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SittingExists(DateTime date, int sittingTypeId)
        {
          return (_context.Sittings?.Any(e => e.Date == date&&e.SittingTypeId==sittingTypeId)).GetValueOrDefault();
        }

        private SittingViewModel GenerateDefaultViewModel()
        {
            //New view modol
            SittingViewModel viewModel = new SittingViewModel();
            //Set sitting to null (if no existing sitting)
            viewModel.Sitting = null!;
            //Populate the selest list items
            viewModel.SittingTypeList = new SelectList(_context.SittingTypes, "Id", "Name");
            viewModel.TimeslotList = new SelectList(_context.Timeslots, "Time", "TimeFormatted");
            //Sitting list - combine Date & SittingType for display

            return viewModel;
        }
    }
}
