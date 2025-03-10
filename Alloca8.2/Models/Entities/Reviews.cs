using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alloca8._2.Models.Entities
{
    public class Reviews
    {
        [Key]
        public Guid ReviewID { get; set; }
        public Guid UserID { get; set; }
        public Guid HotelID { get; set; }
        [Column (TypeName ="decimal(3, 1)")]
        public decimal Rating { get; set; }  // 1-5 scale
        public string Comment { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public Users? Users { get; set; } 
        public Hotels?   Hotels { get; set; }
    }
}
