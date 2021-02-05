using Microsoft.EntityFrameworkCore;
using TRAINING.API.Model;

namespace TRAINING.API.Data
{
    public class APRISEContext : DbContext
    {
        public APRISEContext(DbContextOptions<APRISEContext> options) : base(options) {}        
        public DbSet<EMPLOYEE> EMPLOYEE { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<EMPLOYEE>().HasKey(k => new { k.NIK });
        }
    }
}