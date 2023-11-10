using BeanSceneMVC.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BeanSceneMVC.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<MenuCategory> MenuCategories { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<Table> Tables { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {



        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Area>(e => e.Property(e => e.Id).ValueGeneratedOnAdd());


            builder.Entity<Area>().HasData(
                new Area { Id = 1, Name = "Main" },
                new Area { Id = 2, Name = "Outside" },
                new Area { Id = 3, Name = "Balcony" }
                );
            builder.Entity<Table>().HasData(
                new Table { Code = "M1", AreaId = 1 },
                new Table { Code = "M2", AreaId = 1 },
                new Table { Code = "M3", AreaId = 1 },
                new Table { Code = "M4", AreaId = 1 },
                new Table { Code = "M5", AreaId = 1 },
                new Table { Code = "M6", AreaId = 1 },
                new Table { Code = "M7", AreaId = 1 },
                new Table { Code = "M8", AreaId = 1 },
                new Table { Code = "M9", AreaId = 1 },
                new Table { Code = "M10", AreaId = 1 },
                new Table { Code = "O1", AreaId = 2 },
                new Table { Code = "O2", AreaId = 2 },
                new Table { Code = "O3", AreaId = 2 },
                new Table { Code = "O4", AreaId = 2 },
                new Table { Code = "O5", AreaId = 2 },
                new Table { Code = "O6", AreaId = 2 },
                new Table { Code = "O7", AreaId = 2 },
                new Table { Code = "O8", AreaId = 2 },
                new Table { Code = "O9", AreaId = 2 },
                new Table { Code = "O10", AreaId = 2 },
                new Table { Code = "B1", AreaId = 3 },
                new Table { Code = "B2", AreaId = 3 },
                new Table { Code = "B3", AreaId = 3 },
                new Table { Code = "B4", AreaId = 3 },
                new Table { Code = "B5", AreaId = 3 },
                new Table { Code = "B6", AreaId = 3 },
                new Table { Code = "B7", AreaId = 3 },
                new Table { Code = "B8", AreaId = 3 },
                new Table { Code = "B9", AreaId = 3 },
                new Table { Code = "B10", AreaId = 3 }
                );
            base.OnModelCreating(builder);
        }
    }
}
