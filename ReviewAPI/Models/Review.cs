using System.ComponentModel.DataAnnotations;

namespace ReviewAPI.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required string Title { get; set; }  // 'required' ensures non-nullable property

        [Required]
        public required string Content { get; set; }

        [Required]
        public int Rating { get; set; }  // 1 - 5 stars
    }
}



