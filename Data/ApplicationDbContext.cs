using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StanfordHospital.Models;

namespace StanfordHospital.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) 
        {
        }

        public DbSet<User> users { get; set; }
        public DbSet<Patient> Patient {  get; set; }
        public DbSet<Appointment> Appointment { get; set; }
        public DbSet<Room> Room { get; set; }
        public DbSet<Ipd> Ipd { get; set; }
        public DbSet<Report> Report { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
