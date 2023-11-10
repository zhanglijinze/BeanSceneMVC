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
        public DbSet<Timeslot> Timeslots { get; set; }
        public DbSet<SittingType> SittingTypes { get; set; }
        public DbSet<Sitting> Sittings { get; set; }


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

            builder.Entity<Timeslot>().HasData(
               new Timeslot { Time = new DateTime(2000, 1, 1, 8, 0, 0) },//8:00 AM
               new Timeslot { Time = new DateTime(2000, 1, 1, 8, 30, 0) },//8:30 AM
               new Timeslot { Time = new DateTime(2000, 1, 1, 9, 0, 0) },
               new Timeslot { Time = new DateTime(2000, 1, 1, 9, 30, 0) },
               new Timeslot { Time = new DateTime(2000, 1, 1, 10, 0, 0) },
               new Timeslot { Time = new DateTime(2000, 1, 1, 10, 30, 0) },
               new Timeslot { Time = new DateTime(2000, 1, 1, 11, 0, 0) },
               new Timeslot { Time = new DateTime(2000, 1, 1, 11, 30, 0) },
               new Timeslot { Time = new DateTime(2000, 1, 1, 12, 0, 0) },
               new Timeslot { Time = new DateTime(2000, 1, 1, 12, 30, 0) },
               new Timeslot { Time = new DateTime(2000, 1, 1, 13, 0, 0) },
               new Timeslot { Time = new DateTime(2000, 1, 1, 13, 30, 0) },
               new Timeslot { Time = new DateTime(2000, 1, 1, 14, 0, 0) },
               new Timeslot { Time = new DateTime(2000, 1, 1, 14, 30, 0) },
               new Timeslot { Time = new DateTime(2000, 1, 1, 15, 0, 0) },
               new Timeslot { Time = new DateTime(2000, 1, 1, 15, 30, 0) },
               new Timeslot { Time = new DateTime(2000, 1, 1, 16, 0, 0) },
               new Timeslot { Time = new DateTime(2000, 1, 1, 16, 30, 0) },
               new Timeslot { Time = new DateTime(2000, 1, 1, 17, 0, 0) },
               new Timeslot { Time = new DateTime(2000, 1, 1, 17, 30, 0) },
               new Timeslot { Time = new DateTime(2000, 1, 1, 18, 0, 0) },
               new Timeslot { Time = new DateTime(2000, 1, 1, 18, 30, 0) },
               new Timeslot { Time = new DateTime(2000, 1, 1, 19, 0, 0) },
               new Timeslot { Time = new DateTime(2000, 1, 1, 19, 30, 0) },
               new Timeslot { Time = new DateTime(2000, 1, 1, 20, 0, 0) },
               new Timeslot { Time = new DateTime(2000, 1, 1, 20, 30, 0) },
               new Timeslot { Time = new DateTime(2000, 1, 1, 21, 0, 0) },
               new Timeslot { Time = new DateTime(2000, 1, 1, 21, 30, 0) },
               new Timeslot { Time = new DateTime(2000, 1, 1, 22, 0, 0) }
               );
            builder.Entity<SittingType>().HasData(
               new SittingType { Id = 1, Name = "Breakfast" },
               new SittingType { Id = 2, Name = "Lunch" },
               new SittingType { Id = 3, Name = "Dinner" }
           );

            base.OnModelCreating(builder);
        }
    }
}
