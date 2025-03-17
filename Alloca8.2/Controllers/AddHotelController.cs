using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Alloca8._2.Models.Entities;
using Alloca8._2.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Alloca8._2.Dtos;

namespace Alloca8._2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddHotelController : ControllerBase
    {
        private readonly Alloca8DbContext _context;

        public AddHotelController(Alloca8DbContext context)
        {
            _context = context;
        }

        // ✅ Register a Hotel (Now Uses DTO)
        [HttpPost("AddHotel")]
        public async Task<IActionResult> AddHotel([FromForm] HotelRegistrationDtoClass hotelDTO, [FromForm] IFormFile? imageFile)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Ensure user can only register one hotel
            var existingHotel = await _context.Hotels.FirstOrDefaultAsync(h => h.OwnerID == hotelDTO.OwnerID);
            if (existingHotel != null)
                return BadRequest("User already owns a hotel.");

            // Create new hotel entity
            var newHotel = new Hotels
            {
                HotelID = Guid.NewGuid(),
                Name = hotelDTO.Name,
                Description = hotelDTO.Description,
                OwnerID = hotelDTO.OwnerID,
                CreateDate = DateTime.UtcNow
            };

            // Save hotel first
            _context.Hotels.Add(newHotel);
            await _context.SaveChangesAsync();

            // Handle Image Upload if provided
            if (imageFile != null && imageFile.Length > 0)
            {
                var imageEntity = new HotelImages
                {
                    ImageID = Guid.NewGuid(),
                    HotelID = newHotel.HotelID,
                    ImagePath = await SaveImage(imageFile)
                };

                _context.HotelImages.Add(imageEntity);
                await _context.SaveChangesAsync();
            }

            return CreatedAtAction(nameof(GetHotel), new { id = newHotel.HotelID }, newHotel);
        }


        //  Get all Hotels
        [HttpGet("GetHotels")]
        public async Task<ActionResult<IEnumerable<Hotels>>> GetHotels()
        {
            var hotels = await _context.Hotels.Include(h => h.HotelImages).ToListAsync();
            return Ok(hotels);
        }

        // Get Hotel by ID
        [HttpGet("GetHotel/{id}")]
        public async Task<ActionResult<Hotels>> GetHotel(Guid id)
        {
            var hotel = await _context.Hotels.Include(h => h.HotelImages).FirstOrDefaultAsync(h => h.HotelID == id);

            if (hotel == null)
                return NotFound();

            return hotel;
        }

        //  Delete Hotel
        [HttpDelete("DeleteHotel/{hotelId}")]
        public async Task<IActionResult> DeleteHotel(Guid hotelId)
        {
            var hotelToDelete = await _context.Hotels.Include(h => h.HotelImages).FirstOrDefaultAsync(h => h.HotelID == hotelId);
            if (hotelToDelete == null)
                return NotFound("Hotel not found.");

            // Remove associated images
            _context.HotelImages.RemoveRange(hotelToDelete.HotelImages);

            // Remove hotel
            _context.Hotels.Remove(hotelToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Simple API Ping Test
        [HttpGet("Ping")]
        public IActionResult Ping()
        {
            return Ok("Pong! API is responding.");
        }

        // 🔹 Private method to save image to server
        private async Task<string> SaveImage(IFormFile file)
        {
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var filePath = Path.Combine(uploadsFolder, $"{Guid.NewGuid()}_{file.FileName}");
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return filePath;
        }
    }
}
