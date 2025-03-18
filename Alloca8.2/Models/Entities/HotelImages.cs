using Alloca8._2.Models.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class HotelImages
{
    [Key]
    public Guid ImageID { get; set; } // This was previously int in the DB

    public Guid HotelID { get; set; }
    public Guid? RoomID { get; set; }

    public string ImagePath { get; set; } = string.Empty;

    // Navigation Properties
    [ForeignKey("HotelID")]
    public Hotels? Hotel { get; set; }

    [ForeignKey("RoomID")]
    public Rooms? Room { get; set; }
}
