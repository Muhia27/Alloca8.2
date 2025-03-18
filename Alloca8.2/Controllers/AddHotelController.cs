using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Alloca8._2.Models.Entities;
using Alloca8._2.Data;
using Alloca8._2.Dtos;

namespace Alloca8._2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddHotelController(Alloca8DbContext context, ILogger<AddHotelController> logger) : ControllerBase
    {
        private readonly Alloca8DbContext _context = context;
        private readonly ILogger<AddHotelController> _logger = logger;

        // ✅ Register a Hotel (Fixes GUID Issue & Logging Template)
        [HttpPost("AddHotel")]
        [Authorize]
        public async Task<IActionResult> AddHotel([FromForm] HotelRegistrationDtoClass hotelDTO, [FromForm] IFormFile? imageFile)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // 🔹 Get OwnerID as GUID
            if (!Guid.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var ownerID))
                return Unauthorized("User authentication failed.");

            _logger.LogInformation("Authenticated OwnerID: {OwnerID}", ownerID);

            // 🔹 Ensure User doesn't already own a hotel
            if (await _context.Hotels.AnyAsync(h => h.OwnerID == ownerID))
                return BadRequest("User already owns a hotel.");

            var newHotel = new Hotels
            {
                HotelID = Guid.NewGuid(),
                Name = hotelDTO.Name,
                Description = hotelDTO.Description,
                OwnerID = ownerID,
                CreateDate = DateTime.UtcNow,
                IsFeatured = hotelDTO.IsFeatured,
                IsActive = hotelDTO.IsActive
            };

            _context.Hotels.Add(newHotel);
            await _context.SaveChangesAsync();

            string? imageUrl = null;
            if (imageFile?.Length > 0)
            {
                var imagePath = await SaveImage(imageFile);
                _context.HotelImages.Add(new HotelImages
                {
                    ImageID = Guid.NewGuid(),
                    HotelID = newHotel.HotelID,
                    ImagePath = imagePath
                });
                await _context.SaveChangesAsync();
                imageUrl = imagePath;
            }

            return CreatedAtAction(nameof(GetHotel), new { id = newHotel.HotelID }, new AddHotelResponseDto(
                newHotel.HotelID,
                newHotel.Name,
                newHotel.Description,
                imageUrl,
                newHotel.IsFeatured,
                newHotel.IsActive
            ));
        }

        // ✅ Get all Hotels
        [HttpGet("GetHotels")]
        public async Task<ActionResult<IEnumerable<AddHotelResponseDto>>> GetHotels()
        {
            var hotels = await _context.Hotels
                .Include(h => h.HotelImages)
                .Select(h => new AddHotelResponseDto(
                    h.HotelID,
                    h.Name,
                    h.Description,
                    h.HotelImages.FirstOrDefault()!.ImagePath,
                    h.IsFeatured,
                    h.IsActive
                ))
                .ToListAsync();

            return Ok(hotels);
        }

        // ✅ Get Hotel by ID
        [HttpGet("GetHotel/{id}")]
        public async Task<ActionResult<AddHotelResponseDto>> GetHotel(Guid id)
        {
            var hotel = await _context.Hotels
                .Include(h => h.HotelImages)
                .Where(h => h.HotelID == id)
                .Select(h => new AddHotelResponseDto(
                    h.HotelID,
                    h.Name ?? string.Empty,
                    h.Description ?? string.Empty,
                    h.HotelImages.FirstOrDefault()!.ImagePath,
                    h.IsFeatured,
                    h.IsActive
                ))
                .FirstOrDefaultAsync();

            return hotel is null ? NotFound() : Ok(hotel);
        }

        // ✅ Delete Hotel (Only Owner Can Delete)
        [HttpDelete("DeleteHotel/{hotelId}")]
        [Authorize]
        public async Task<IActionResult> DeleteHotel(Guid hotelId)
        {
            var hotelToDelete = await _context.Hotels
                .Include(h => h.HotelImages)
                .FirstOrDefaultAsync(h => h.HotelID == hotelId);

            if (hotelToDelete is null)
                return NotFound("Hotel not found.");

            // 🔹 Ensure only the owner can delete
            if (!Guid.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var ownerID) || hotelToDelete.OwnerID != ownerID)
                return Forbid("You are not authorized to delete this hotel.");

            _context.HotelImages.RemoveRange(hotelToDelete.HotelImages);
            _context.Hotels.Remove(hotelToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // ✅ API Health Check
        [HttpGet("Ping")]
        public IActionResult Ping() => Ok("Pong! API is responding.");

        // 🔹 Save Image to Server
        private static async Task<string> SaveImage(IFormFile file)
        {
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
            Directory.CreateDirectory(uploadsFolder);

            var filePath = Path.Combine(uploadsFolder, $"{Guid.NewGuid()}_{file.FileName}");
            await using var fileStream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(fileStream);

            return "/images/" + Path.GetFileName(filePath);
        }
    }
}
