public class HotelDTO
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public Guid OwnerID { get; set; } // Required Owner ID
}
