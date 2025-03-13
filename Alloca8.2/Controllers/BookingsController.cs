using Alloca8._2.Data;
using Alloca8._2.Dtos;
using Alloca8._2.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Alloca8._2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly Alloca8DbContext _context;
        public BookingsController(Alloca8DbContext context)
        {
            _context = context;
        }
        [HttpPost]
        public IActionResult CreateBooking(BookingCreateDto bookingDto)
        {
            var booking = new Bookings
            {
                RoomID = bookingDto.RoomId,
                UserID = bookingDto.UserId,
                CheckInDate = bookingDto.StartDate,
                CheckOutDate = bookingDto.EndDate,
            };
            _context.Add(booking);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetBooking), new { id = booking.BookingID }, booking);
        }
        [HttpGet("{id}")]
        public IActionResult GetBooking(Guid id) {
            var booking = _context.Bookings.Find(id);
            if (booking == null)
            {
                return NotFound();
            }
            return Ok(booking);

        }
    }
}
