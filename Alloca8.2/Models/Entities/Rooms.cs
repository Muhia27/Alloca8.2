using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alloca8._2.Models.Entities
{
    public class Rooms
    {
        [Key]
        public Guid RoomID { get; set; }

        [ForeignKey("Hotel")]
        public Guid HotelID { get; set; }

        public string RoomType { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        public int Capacity { get; set; }

        public bool Availability { get; set; } = true; // Default to available

        // Navigation Properties
        public Hotels? Hotel { get; set; }

        public ICollection<Bookings> Bookings { get; set; } = new List<Bookings>();

        public ICollection<HotelImages> RoomImages { get; set; } = new List<HotelImages>(); // Added Room Images
    }
}
