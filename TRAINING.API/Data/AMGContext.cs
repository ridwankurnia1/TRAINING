using Microsoft.EntityFrameworkCore;
using TRAINING.API.Model;

namespace TRAINING.API.Data
{
    public class AMGContext : DbContext
    {
        public AMGContext(DbContextOptions<AMGContext> options) : base(options) {}
        public DbSet<MEMP> MEMP { get; set; }
        public DbSet<MGRD> MGRD { get; set; }
        public DbSet<GOG1> GOG1 { get; set; }
        public DbSet<ZUSR> ZUSR { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ZUSR>().HasKey(k => new { k.ZUUSNO });
            builder.Entity<MEMP>().HasKey(k => new { k.EMEMNO, k.EMBRNO });
            builder.Entity<MGRD>().HasKey(k => new { k.GDEGNO });
            builder.Entity<GOG1>().HasKey(k => new { k.GOOGNO });
            builder.Entity<MEMP>()
                .HasOne(p => p.GOG1)
                .WithMany(p => p.MEMP)
                .HasForeignKey(p => new { p.EMDENO });
        }
    }
}
