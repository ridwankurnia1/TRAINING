using Microsoft.EntityFrameworkCore;
using TRAINING.API.Model;

namespace TRAINING.API.Data
{
    public class AMGContext : DbContext
    {
        public AMGContext(DbContextOptions<AMGContext> options) : base(options) { }
        public DbSet<MEMP> MEMP { get; set; }
        public DbSet<MGRD> MGRD { get; set; }
        public DbSet<GOG1> GOG1 { get; set; }
        public DbSet<SCMI> SCMI { get; set; }
        public DbSet<XUSR> XUSR { get; set; }
        public DbSet<ZUSR> ZUSR { get; set; }
        public DbSet<IPTY> IPTY { get; set; }
        public DbSet<MDF0> MDF0 { get; set; }
        public DbSet<GCT2> GCT2 { get; set; }
        public DbSet<GCUR> GCUR { get; set; }
        public DbSet<IUOM> IUOM { get; set; }
        public DbSet<ZVAR> ZVAR { get; set; }
        public DbSet<IWGRX> IWGRX { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<SCMI>().HasKey(k => new { k.CXCONO, k.CXBRNO, k.CXITNO, k.CXCUNO, k.CXCUIT });
            builder.Entity<XUSR>().HasKey(k => new { k.XUUSNO });
            builder.Entity<ZUSR>().HasKey(k => new { k.ZUUSNO });
            builder.Entity<MEMP>().HasKey(k => new { k.EMEMNO, k.EMBRNO });
            builder.Entity<MGRD>().HasKey(k => new { k.GDEGNO });
            builder.Entity<GOG1>().HasKey(k => new { k.GOOGNO });
            builder.Entity<MEMP>()
                .HasOne(p => p.GOG1)
                .WithMany(p => p.MEMP)
                .HasForeignKey(p => new { p.EMDENO });
            builder.Entity<IPTY>().HasKey(k => new { k.HSCONO, k.HSBRNO, k.HSPETY });
            builder.Entity<MDF0>().HasKey(k => new { k.DDTRID });
            builder.Entity<GCT2>().HasKey(k => new { k.CBKYNO });
            builder.Entity<GCUR>().HasKey(k => new { k.GGCYNO });
            builder.Entity<IUOM>().HasKey(k => new { k.HUUMNO });
            builder.Entity<ZVAR>().HasKey(k => new { k.ZRVANA });
            builder.Entity<IWGRX>().HasKey(k => new { k.HVWHGR });
        }
    }
}
