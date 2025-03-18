namespace Alloca8._2.Dtos
{
    public class HotelRegistrationDtoClass
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public Guid OwnerID { get; set; } // Required Owner ID
        public bool IsFeatured { get; set; }
        public bool IsActive { get; set; }
    }
}