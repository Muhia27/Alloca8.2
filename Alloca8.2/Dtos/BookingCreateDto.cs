namespace Alloca8._2.Dtos
{
    //Create Booking Endpoint
    public class BookingCreateDto
    {
        public Guid RoomId { get; set; }
        public Guid UserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
