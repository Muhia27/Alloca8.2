using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alloca8._2.Models.Entities
{
    public class Bookings
    {
        [Key]
        public Guid BookingID { get; set; }
        public Guid UserID { get; set; }
        public Guid RoomID { get; set; }
        public Guid HotelID { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        [Column(TypeName ="decimal(18, 2)")]
        public decimal TotalPrice { get; set; }

        public Users? Users { get; set; }
        public Rooms? Rooms { get; set; }
        public Hotels? Hotels { get; set; }
        public ICollection<Payments> Payments { get; set; } = new List<Payments>();
    }
}
