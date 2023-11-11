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


namespace BeanSceneMVC.Controllers
{
    public class SittingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SittingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Sittings
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
        public IActionResult Create()
        {
            ViewData["EndTimeId"] = new SelectList(_context.Timeslots, "Time", "Time");
            ViewData["SittingTypeId"] = new SelectList(_context.SittingTypes, "Id", "Name");
            ViewData["StartTimeId"] = new SelectList(_context.Timeslots, "Time", "Time");
         /*   return View();*/
           return View( GenerateDefaultViewModel());
        }

        // POST: Sittings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Date,SittingTypeId,StartTimeId,EndTimeId,Status,Capacity")] Sitting sitting)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sitting);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EndTimeId"] = new SelectList(_context.Timeslots, "Time", "Time", sitting.EndTimeId);
            ViewData["SittingTypeId"] = new SelectList(_context.SittingTypes, "Id", "Name", sitting.SittingTypeId);
            ViewData["StartTimeId"] = new SelectList(_context.Timeslots, "Time", "Time", sitting.StartTimeId);
            return View(sitting);
        }

        // GET: Sittings/Edit/5/yyyy-mm-dd
        [HttpGet("Sitting/Edit/{sittingTypeId}/{dateString}")]
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


            ViewData["EndTimeId"] = new SelectList(_context.Timeslots, "Time", "Time", sitting.EndTimeId);
            ViewData["SittingTypeId"] = new SelectList(_context.SittingTypes, "Id", "Name", sitting.SittingTypeId);
            ViewData["StartTimeId"] = new SelectList(_context.Timeslots, "Time", "Time", sitting.StartTimeId);
            return View(sitting);
        }

        // POST: Sittings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DateTime id, [Bind("Date,SittingTypeId,StartTimeId,EndTimeId,Status,Capacity")] Sitting sitting)
        {
            if (id != sitting.Date)
            {
                return NotFound();
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
                    if (!SittingExists(sitting.Date))
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
            ViewData["EndTimeId"] = new SelectList(_context.Timeslots, "Time", "Time", sitting.EndTimeId);
            ViewData["SittingTypeId"] = new SelectList(_context.SittingTypes, "Id", "Name", sitting.SittingTypeId);
            ViewData["StartTimeId"] = new SelectList(_context.Timeslots, "Time", "Time", sitting.StartTimeId);
            return View(sitting);
        }

        // GET: Sittings/Delete/1/yyyy-mm-dd
        [HttpGet("Sitting/Delete/{sittingTypeId}/{dateString}")]
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

        // POST: Sittings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(DateTime id)
        {
            if (_context.Sittings == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Sittings'  is null.");
            }
            var sitting = await _context.Sittings.FindAsync(id);
            if (sitting != null)
            {
                _context.Sittings.Remove(sitting);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SittingExists(DateTime id)
        {
          return (_context.Sittings?.Any(e => e.Date == id)).GetValueOrDefault();
        }

        private SittingViewModel GenerateDefaultViewModel()
        {
            //New view modol
            SittingViewModel viewModel = new SittingViewModel();
            //Set session to null (if existing session)
            viewModel.Sitting = null!;
            //Populate the selest list items
            viewModel.SittingTypeList = new SelectList(_context.SittingTypes, "Id", "Name");
            viewModel.TimeslotList = new SelectList(_context.Timeslots, "Time", "TimeFormatted");
            return viewModel;
        }
    }
}
