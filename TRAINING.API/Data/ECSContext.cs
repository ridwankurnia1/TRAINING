using Microsoft.EntityFrameworkCore;
using TRAINING.API.Model;

namespace TRAINING.API.Data
{
    public class ECSContext : DbContext
    {
        public ECSContext(DbContextOptions<ECSContext> options) : base(options) {}
        public DbSet<EHAL> EHAL { get; set; }
        public DbSet<ELOH> ELOH { get; set; }
        public DbSet<ELOG> ELOG { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ELOH>().HasKey(k => new { k.EHRCID });
            builder.Entity<ELOG>().HasKey(k => new { k.ELTRID });
            builder.Entity<EHAL>().HasKey(k => new { k.ELEMNO });

            builder.Entity<ELOH>()
                .HasMany(x => x.ELOG)
                .WithOne(x => x.ELOH)
                .HasForeignKey(x => x.ELRCID);
        }
    }
}