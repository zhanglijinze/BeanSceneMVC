using BeanSceneMVC.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BeanSceneMVC.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<MenuCategory> MenuCategories{ get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Area> Areas { get; set; }  
        public DbSet<Table> Tables { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options) 
        { 
        
        
        
        }
    }
}
