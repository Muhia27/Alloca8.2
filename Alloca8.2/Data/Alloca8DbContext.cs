using Alloca8._2.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Alloca8._2.Data
{
    public class Alloca8DbContext : IdentityDbContext<Users,IdentityRole<Guid>,Guid>
    {
        public Alloca8DbContext(DbContextOptions options) : base(options)
        {
            
        }
      
        public DbSet<Rooms> Rooms { get; set; }
        public DbSet<Bookings>Bookings { get; set; }
        public DbSet<Reviews> Reviews { get; set; }
        public DbSet<Payments> Payments { get; set; }
        public DbSet<HotelImages> HotelImagees { get; set; }
        public DbSet<Hotels> Hotels { get; set; }

    }
}
