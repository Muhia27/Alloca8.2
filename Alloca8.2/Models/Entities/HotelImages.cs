using System.ComponentModel.DataAnnotations;

namespace Alloca8._2.Models.Entities
{
    public class HotelImages
    {
        [Key]
        public int ImageID { get; set; }  // Primary Key
        public int HotelID { get; set; }  // Foreign Key to Hotels
        public int RoomID { get; set; }  // Foreign Key to Rooms
        public string? ImageURL { get; set; }  // Image location

        public Hotels? Hotels { get; set; }
        public Rooms? Rooms { get; set; }
    }

}
