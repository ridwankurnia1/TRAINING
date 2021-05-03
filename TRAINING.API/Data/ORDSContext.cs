using Microsoft.EntityFrameworkCore;
using TRAINING.API.Model;

namespace TRAINING.API.Data
{
    public class ORDSContext : DbContext
    {
        public ORDSContext(DbContextOptions<ORDSContext> options) : base(options) {}        
        public DbSet<KTPA02> KTPA02 { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<KTPA02>().HasKey(k => new { k.RECTA02, k.DONOA02 });
        }
    }
}