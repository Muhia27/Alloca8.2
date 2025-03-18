using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alloca8._2.Models.Entities
{
    public class Hotels
    {
        [Key]
        public Guid UserId { get; set; }

    

        [Required] // Ensures name cannot be null
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public DateTime CreateDate { get; set; } = DateTime.UtcNow;

        // NEW PROPERTIES
        public bool IsFeatured { get; set; } = false; // Default to not featured
        public bool IsActive { get; set; } = true;   // Default to active

        // Navigation Properties
        [ForeignKey("UsersId")]
        public Users? HotelOwner { get; set; }


        public ICollection<Rooms> Rooms { get; set; } = new List<Rooms>();

        public ICollection<HotelImages> HotelImages { get; set; } = new List<HotelImages>();

        public ICollection<Reviews> Reviews { get; set; } = new List<Reviews>();
    }
}