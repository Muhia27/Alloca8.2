using Alloca8._2.Models.Entities;

namespace Alloca8._2.Dtos
{
    public class UserRegistrationDto
    {
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public UserRole Role { get; set; }
        public string? ImageUrl { get; set; }
    }
}
