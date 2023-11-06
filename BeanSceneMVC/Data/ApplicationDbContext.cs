using BeanSceneMVC.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BeanSceneMVC.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<MenuCategory> MenuCategories{ get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options) 
        { 
        
        
        
        }
    }
}
