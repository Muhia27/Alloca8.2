using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alloca8._2.Models.Entities
{
    public class Hotels
    {
        [Key]
        public Guid HotelID { get; set; }

        public Guid OwnerID { get; set; } // Foreign key to Users table

        [Required] // Ensures name cannot be null
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public DateTime CreateDate { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        [ForeignKey("OwnerID")]
        public Users? HotelOwner { get; set; }

        public ICollection<Rooms> Rooms { get; set; } = new List<Rooms>();

        public ICollection<HotelImages> HotelImages { get; set; } = new List<HotelImages>();

        public ICollection<Reviews> Reviews { get; set; } = new List<Reviews>();
    }
}
