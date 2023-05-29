using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RoyalFinalApp.Models;

namespace RoyalFinalApp.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Topbar> Topbars { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Hall> Halls { get; set; }
        public DbSet<Planner> Planners { get; set; }
        public DbSet<Testimonial> Testimonials { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<AboutUs> AboutUs { get; set; }
    }
}
