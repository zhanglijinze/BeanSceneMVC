
using BeanSceneMVC.Data;
using BeanSceneMVC.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeanScene.Tests.Infrastructure
{
    public class BeanSceneTestBase: IDisposable
    {
        protected readonly ApplicationDbContext _context;
        protected readonly IWebHostEnvironment _webHostEnvironment;

        public BeanSceneTestBase() 
        {

            //DB context options - use in-memory database with random name (each unit test uses different DB)

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                    .Options;

        //store DB context

            _context = new ApplicationDbContext(options);
        //Cleanup
           _context.Database.EnsureCreated();

        //Exit if DB already has data

            if (_context.MenuCategories.Any()) return;

        //Seed data
        //Todo...
        SeedMenuCategoriesData();
            SeedMenuItemsData();

        }

        private void SeedMenuCategoriesData()
        {
            //MenuCategories collection
            var menuCategories = new[]
            {
              new MenuCategory { Id = 1, Name="Mains"},
              new MenuCategory { Id = 2, Name="Entrees"},
              new MenuCategory { Id = 3, Name="Desserts"},
              new MenuCategory { Id = 4, Name="Drinks"},
              new MenuCategory { Id = 5, Name="Sides"},
            
         };

            _context.MenuCategories.AddRange(menuCategories);
            _context.SaveChanges();

        }

        private void SeedMenuItemsData()
        {
            //MenuCategories collection
            var menuItems = new[]
            {
              new MenuItem { 
                  
              Id = 1, 
                  
              Name="Mains",
              
              Description="Main-1 test",
              
              Price =13,

              Photo = "Main-1.jpg",

              IsAllergenFree= true,

              IsDiaryFree= true,

              IsGlutenFree= true,

              IsVegan= true,

              IsVegetarian= true,

              MenuCategoryId= 1,
              
              },

              new MenuItem {

              Id = 2,

              Name="Entrees",

              Description="Entrees-1 test",

              Price =12,

              Photo = "Entrees-1.jpg",

              IsAllergenFree= false,

              IsDiaryFree= true,

              IsGlutenFree= true,

              IsVegan= true,

              IsVegetarian= true,

              MenuCategoryId= 2,

              },

              new MenuItem {

              Id = 3,

              Name="Desserts",

              Description="Desserts-1 test",

              Price =12,

              Photo = "Desserts-1.jpg",

              IsAllergenFree= false,

              IsDiaryFree= false,

              IsGlutenFree= true,

              IsVegan= true,

              IsVegetarian= true,

              MenuCategoryId= 3,

              },



         };

            _context.MenuItems.AddRange(menuItems);
            _context.SaveChanges();

        }

        public void Dispose() 
        {
            //Cleanup resources used by each unit test
            _context.Database.EnsureDeleted();
            _context.Dispose();

            // Notify the GC that the finalizer is no longer necessary
            GC.SuppressFinalize(this);
        }


    }
}
