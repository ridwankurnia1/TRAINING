using Microsoft.EntityFrameworkCore;
using TRAINING.API.Model;

namespace TRAINING.API.Data
{
    public class ECSContext : DbContext
    {
        public ECSContext(DbContextOptions<ECSContext> options) : base(options) {}
        public DbSet<EHAL> EHAL { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<EHAL>().HasKey(k => new { k.ELEMNO });
        }
    }
}