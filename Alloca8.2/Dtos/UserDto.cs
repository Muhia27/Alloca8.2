using Alloca8._2.Models.Entities;

namespace Alloca8._2.Dtos
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public required string UserName { get; set; }
        public required  string Email { get; set; }
        public string? ImageUrl { get; set; }
        public UserRole Role { get; set; }
        public DateTime CreateDate { get; set; }

    }
}
