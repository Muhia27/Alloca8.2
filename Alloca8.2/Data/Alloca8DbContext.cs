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
        public DbSet<HotelImages> HotelImages { get; set; }
        public DbSet<Hotels> Hotels { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Hotels>()
             .HasOne(h => h.HotelOwner)
              .WithMany(u => u.Hotels)
             .HasForeignKey(h => h.UserId) // Changed to UserId
             .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<HotelImages>()
                .HasOne(h => h.Hotel)
                .WithMany(h => h.HotelImages)
                .HasForeignKey(h => h.HotelID)
                .OnDelete(DeleteBehavior.NoAction); // ❌ No Cascade Delete for Hotel

            modelBuilder.Entity<HotelImages>()
                .HasOne(h => h.Room)
                .WithMany(r => r.RoomImages)
                .HasForeignKey(h => h.RoomID)
                .OnDelete(DeleteBehavior.SetNull); // ✅ Keep RoomID as NULL if Room is deleted
        }


    }
}
