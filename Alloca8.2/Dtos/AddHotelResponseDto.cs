namespace Alloca8._2.Dtos
{
    public class AddHotelResponseDto(
        Guid HotelID,
        string? Name,
        string? Description,
        string? ImageUrl,
        bool IsFeatured,
        bool IsActive
    )
    {
        public Guid HotelID { get; set; } = HotelID;
        public string? Name { get; set; } = Name;
        public string? Description { get; set; } = Description;
        public string? ImageUrl { get; set; } = ImageUrl;
        public bool IsFeatured { get; set; } = IsFeatured;
        public bool IsActive { get; set; } = IsActive;
    }
}