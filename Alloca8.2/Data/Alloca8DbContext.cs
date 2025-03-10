using Alloca8._2.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Alloca8._2.Data
{
    public class Alloca8DbContext : DbContext
    {
        public Alloca8DbContext(DbContextOptions options) : base(options)
        {
            
        }
        public DbSet<Users> Users { get; set; }
        public DbSet<Rooms> Rooms { get; set; }
        public DbSet<Bookings>Bookings { get; set; }
        public DbSet<Reviews> Reviews { get; set; }
        public DbSet<Payments> Payments { get; set; }
        public DbSet<HotelImages> HotelImagees { get; set; }
        public DbSet<Hotels> Hotels { get; set; }

    }
}
