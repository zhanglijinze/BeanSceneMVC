using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BeanSceneMVC.Data;
using BeanSceneMVC.Models;
using System.Diagnostics.Eventing.Reader;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;

namespace BeanSceneMVC.Controllers
{
    public class MenuItemsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public MenuItemsController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;//Info about web hosting environment to generate paths on the server (saving images)
        }

        // GET: MenuItems
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.MenuItems.Include(m => m.MenuCategory);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: MenuItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MenuItems == null)
            {
                return NotFound();
            }

            var menuItem = await _context.MenuItems
                .Include(m => m.MenuCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (menuItem == null)
            {
                return NotFound();
            }

            return View(menuItem);
        }

        // GET: MenuItems/Details/5
        public async Task<IActionResult> DetailsCustomer(int? id)
        {
            if (id == null || _context.MenuItems == null)
            {
                return NotFound();
            }

            var menuItem = await _context.MenuItems
                .Include(m => m.MenuCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (menuItem == null)
            {
                return NotFound();
            }

            return View(menuItem);
        }


        // GET: MenuItems/Create
        public async Task<IActionResult> Create(int? id)
        {
            MenuItem? menuItem = null;

            if (id != null)
            {
                menuItem = await _context.MenuItems.FindAsync(id);
                if (menuItem == null)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            ViewData["MenuCategoryId"] = new SelectList(_context.MenuCategories, "Id", "Name");
            return View(menuItem);
        }

        // POST: MenuItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Price,Photo,IsGlutenFree,IsDiaryFree,IsVegetarian,IsVegan,IsAllergenFree,MenuCategoryId")] MenuItem menuItem)
        {
            MenuCategory? menuCategory = await _context.MenuCategories.FindAsync(menuItem.MenuCategoryId);
            if (menuCategory == null) 
            {
                return NotFound("Menu category not found");
            }
            menuItem.MenuCategory = menuCategory;

            ModelState.Clear();
            TryValidateModel(menuItem);

            //check for uniqueness of the Name index

            if (null != await _context.MenuItems.FirstOrDefaultAsync(Item => Item.Name.ToLower() == menuItem.Name.ToLower()))
            {
                ModelState.AddModelError("Name", "Name already exists");
            }

            if (ModelState.IsValid)
            {
                _context.Add(menuItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MenuCategoryId"] = new SelectList(_context.MenuCategories, "Id", "Name", menuItem.MenuCategoryId);
            return View(menuItem);
        }

        // GET: MenuItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MenuItems == null)
            {
                return NotFound();
            }

            var menuItem = await _context.MenuItems.FindAsync(id);
            if (menuItem == null)
            {
                return NotFound();
            }
            ViewData["MenuCategoryId"] = new SelectList(_context.MenuCategories, "Id", "Name", menuItem.MenuCategoryId);
            return View(menuItem);
        }

        // POST: MenuItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Price,Photo,IsGlutenFree,IsDiaryFree,IsVegetarian,IsVegan,IsAllergenFree,MenuCategoryId")] MenuItem menuItem)
        {
            if (id != menuItem.Id)
            {
                return BadRequest("Id do not match");
            }

            MenuCategory? menuCategory = await _context.MenuCategories.FindAsync(menuItem.MenuCategoryId);
            if (menuCategory == null)
            {
                return NotFound($"Menu category not found with ID:{menuItem.MenuCategoryId}");
            }
            menuItem.MenuCategory = menuCategory;

            ModelState.Clear();
            TryValidateModel(menuItem);


            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(menuItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MenuItemExists(menuItem.Id))
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
            ViewData["MenuCategoryId"] = new SelectList(_context.MenuCategories, "Id", "Name", menuItem.MenuCategoryId);
            return View(menuItem);
        }

        // GET: MenuItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MenuItems == null)
            {
                return NotFound();
            }

            var menuItem = await _context.MenuItems
                .Include(m => m.MenuCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (menuItem == null)
            {
                return NotFound();
            }

            return View(menuItem);
        }

        // POST: MenuItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MenuItems == null)
            {
                return Problem("Entity set 'ApplicationDbContext.MenuItems'  is null.");
            }
            var menuItem = await _context.MenuItems.FindAsync(id);
            if (menuItem != null)
            {
                _context.MenuItems.Remove(menuItem);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MenuItemExists(int id)
        {
          return (_context.MenuItems?.Any(e => e.Id == id)).GetValueOrDefault();
        }


        public enum Sort {
            [Display(Name = "Lowest to Highest")]
            Lowest_Highest,
            [Display(Name = "Highest to lowest")]
            Highest_Lowest,
        }
        // GET: MenuItems/ViewAll
        public async Task<IActionResult> ViewAll(string?search, int? menuCategoryId,string?sort)
        {
            var sortPrice = Enum.GetValues(typeof(Sort)).Cast<Sort>();
            ViewBag.MyPriceSort = new SelectList(sortPrice);

            IQueryable<MenuItem> menuItems = _context.MenuItems;

       
            if (!string.IsNullOrEmpty(search))
            {
           
                menuItems = menuItems.Where(m => m.Name.Contains(search));
            }
            if (menuCategoryId != null)
            {
              
                menuItems = menuItems.Where(m => m.MenuCategoryId == menuCategoryId);

            }
            if (sort != null)
            {
                if (sort == "Lowest_Highest")
                { menuItems = menuItems.OrderBy(m => m.Price); } 
                else{ menuItems = menuItems.OrderByDescending(m => m.Price); }

            }
            else { menuItems = menuItems.OrderBy(m => m.Name); }

            ViewData["MenuCategoryList"] = new SelectList(_context.MenuCategories, "Id", "Name");
                             
            return View(await menuItems.Include(m => m.MenuCategory).ToListAsync());

          
            var menuItemsWithMenuCategory = menuItems = _context.MenuItems.Include(m => m.MenuCategory);

          
            if (!string.IsNullOrEmpty(search))
            {
                // Filter menu results
                menuItemsWithMenuCategory = menuItemsWithMenuCategory.Where(m => m.Name.Contains(search))
                    //.Where(b => b.Title.ToLower().Contains(search.ToLower()))
                    .Include(m => m.MenuCategory);
            }

            // Load view with menus
            return View(await menuItemsWithMenuCategory.ToListAsync());
        }

        //Post:MenuItem/ImageUpload
        //Handle image upload for menuItem
        [HttpPost, ActionName("ImageUpload")]
        public async Task<IActionResult> ImageUpload(IFormFile file)
        {   //Image save folder within wwwroot

            string imageFoler = "Images/Menu";

            //Create folder if it doesn't exist
            string dirPath = Path.Combine(_webHostEnvironment.WebRootPath, imageFoler);
            if (!Directory.Exists(dirPath)) Directory.CreateDirectory(dirPath);

            //Create image file (handle the upload)
            string filePath = Path.Combine(dirPath, file.FileName);
            using (FileStream fs = System.IO.File.Create(filePath))
            {
                file.CopyTo(fs);
            }


                //return 200+image path

               /* return StatusCode(200, $"/{imageFoler}/{file.FileName}");*/

            return StatusCode(200, $"{file.FileName}");
        }
    }
}
