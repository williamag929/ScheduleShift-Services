using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShiftWork.Backend.Models;

namespace ShiftWork.Backend.Data
{
    public class ShiftWorkContext : DbContext
    {
        public ShiftWorkContext (DbContextOptions<ShiftWorkContext> options)
            : base(options)
        {
        }

        public DbSet<ShiftWork.Backend.Models.Person> Person { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().ToTable("Person");
        }

        public DbSet<ShiftWork.Backend.Models.Location>? Locations { get; set; }
        public DbSet<ShiftWork.Backend.Models.Area>? Areas { get; set; }
        public DbSet<ShiftWork.Backend.Models.Role>? Roles { get; set; }
        public DbSet<ShiftWork.Backend.Models.TaskShift>? TaskShifts { get; set; }
        public DbSet<ShiftWork.Backend.Models.ScheduleShift>? ScheduleShifts { get; set; }
        public DbSet<ShiftWork.Backend.Models.Schedule>? Schedules { get; set; }
        public DbSet<ShiftWork.Backend.Models.Company>? Companies { get; set; }
        public DbSet<ShiftWork.Backend.Models.Permission>? Permissions { get; set; }
        public DbSet<ShiftWork.Backend.Models.RolePermission>? RolePermissions { get; set; }
        public DbSet<ShiftWork.Backend.Models.UserCompany>? userCompanies { get; set; }
        public DbSet<ShiftWork.Backend.Models.UserProfile>? UserProfiles { get; set; }
        public DbSet<ShiftWork.Backend.Models.UserRole>? userRoles { get; set; }
        public DbSet<ShiftWork.Backend.Models.Country>? Countries { get; set; }
        public DbSet<ShiftWork.Backend.Models.Time_Zone>? TimeZones { get; set; }




    }
}
