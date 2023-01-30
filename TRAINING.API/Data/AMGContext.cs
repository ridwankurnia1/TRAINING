using System;
using System.Collections.Generic;
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
        public DbSet<IWHSX> IWHSX { get; set; }


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
            builder.Entity<IWHSX>().HasKey(k => new { k.HWWHNO });
            builder.Entity<IWHSX>().HasOne(p => p.IWGRX).WithOne(x => x.IWHSX).HasForeignKey<IWHSX>(p => new { p.HWWHGR });

            // SeedWarehouse(builder);
        }

        private void SeedWarehouse(ModelBuilder builder)
        {
            var whgList = new List<IWGRX>();

            for (int i = 0; i < 10; i++)
            {
                whgList.Add(new IWGRX
                {
                    HVWHGR = Guid.NewGuid().ToString().Substring(0, 9),
                    HVGRNA = Faker.Company.Name(),
                    HVBRNO = "CKP",
                    HVREMA = Faker.Lorem.Paragraph(3),
                    HVRCST = Faker.RandomNumber.Next(1)
                });
            }

            var whList = new List<IWHSX>();

            foreach (var whg in whgList)
            {
                for (int i = 0; i < 10; i++)
                {
                    whList.Add(new IWHSX
                    {
                        HWWHNO = Guid.NewGuid().ToString().Substring(0, 9),
                        HWWHNA = Faker.Company.Name(),
                        HWNICK = Faker.Lorem.GetFirstWord().ToLower(),
                        HWWHGR = whg.HVGRNA,
                        HWDFWH = Faker.Country.Name().ToLower(),
                        HWFDAY = Faker.RandomNumber.Next(0, 100),
                        HWFIFO = Faker.RandomNumber.Next(0, 1),
                        HWRCST = Faker.RandomNumber.Next(0, 1)
                    });
                }
            }

            builder.Entity<IWGRX>().HasData(whgList);
            builder.Entity<IWHSX>().HasData(whList);
        }
    }
}
