using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alloca8._2.Models.Entities
{
    public enum PaymentMethod
    {
        CreditCard,
        PayPal,
        MobileMoney
    }

    public enum PaymentStatus
    {
        Pending,
        Completed,
        Failed
    }

    public class Payments
    {
        [Key]
        public Guid PaymentID { get; set; }  // Primary Key
        public Guid BookingID { get; set; }  // Foreign Key
        public Guid UserID { get; set; }  // Foreign Key
        [Column (TypeName ="decimal (18, 2)")]

        public decimal Amount { get; set; }
        public PaymentMethod Method { get; set; }
        public PaymentStatus Status { get; set; } = PaymentStatus.Pending;
        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;

        public Bookings? Bookings { get; set; }
        public Users? Users { get; set; }
    }
}
