using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alloca8._2.Models.Entities
{
    public class Hotels
    {
        [Key]
        public Guid HotelID { get; set; }
        public Guid OwnerID { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        [Column(TypeName ="decimal(3, 1)")]
        public decimal StarRating { get; set; }
        public DateTime CreateDate { get; set; }


        public Users? HotelOwner { get; set; }
        public ICollection<Rooms> Rooms { get; set; } = new List<Rooms>();
        public ICollection<HotelImages> HotelImages { get; set; } = new List<HotelImages>();
        public ICollection<Reviews> Reviews { get; set; } = new List<Reviews>();
    }
}
