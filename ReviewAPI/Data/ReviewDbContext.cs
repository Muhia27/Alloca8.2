using Microsoft.EntityFrameworkCore;
using ReviewAPI.Models;

namespace ReviewAPI.Data
{
    public class ReviewDbContext : DbContext
    {
        public ReviewDbContext(DbContextOptions<ReviewDbContext> options) : base(options) { }

        public DbSet<Review> Reviews { get; set; }
    }
}

