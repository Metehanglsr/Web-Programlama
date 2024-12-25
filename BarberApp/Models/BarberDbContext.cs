using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

namespace BarberApp.Models
{
    public class BarberDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public BarberDbContext(DbContextOptions<BarberDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointment>()
                        .Property(a => a.AppointmentDate)
                        .HasConversion(
                            v => v.ToUniversalTime(),
                            v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
            modelBuilder.Entity<ServiceAppointment>()
                .HasKey(sa => new { sa.AppointmentID, sa.ServiceID });
            modelBuilder.Entity<IdentityRole<int>>()
               .HasData(
                   new IdentityRole<int> { Id = 1, Name = "Admin", NormalizedName = "ADMIN" },
                   new IdentityRole<int> { Id = 2, Name = "Customer", NormalizedName = "CUSTOMER" }
               );
            modelBuilder.Entity<IdentityUserLogin<int>>().HasKey(l => new { l.LoginProvider, l.ProviderKey });
            modelBuilder.Entity<IdentityUserRole<int>>().HasKey(r => new { r.UserId, r.RoleId });
            modelBuilder.Entity<IdentityUserToken<int>>().HasKey(t => new { t.UserId, t.LoginProvider, t.Name });

        }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Barber> Barbers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users{ get; set; }
        public DbSet<Expanse> Expanses { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceAppointment> ServiceAppointments{ get; set; }


    }
}
