using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Alloca8._2.Models.Entities
{
    public enum UserRole
    {
        customer = 1,
        HotelOwner = 2,
        Admin = 3,
    }

    public class Users : IdentityUser<Guid>
    {

        public UserRole Role { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;

        public ICollection<Hotels> Hotels { get; set; } = new List<Hotels>();
        public ICollection<Bookings> Bookings { get; set; } = new List<Bookings>();
        public ICollection<Reviews> Reviews { get; set; } = new List<Reviews>();
        public ICollection<Payments> Payments { get; set; } = new List<Payments>();

        public Users()
        {
            // Initialize collection
            Hotels = new List<Hotels>();
            Bookings = new List<Bookings>();
            Reviews = new List<Reviews>();
            Payments = new List<Payments>();
        }
    }
}