using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TRAINING.API.Model;

namespace TRAINING.API.Data
{
    public class SalesRepository : ISalesRepository
    {
        private readonly IDbContextFactory<AMGContext> _contextFactory;
        // private readonly AMGContext _context;

        public SalesRepository(IDbContextFactory<AMGContext> context)
        {
            _contextFactory = context;
            // _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            using (AMGContext context = _contextFactory.CreateDbContext())
            {
                context.Add(entity);
            }
        }

        public void Delete<T>(T entity) where T : class
        {
            using (AMGContext context = _contextFactory.CreateDbContext())
            {
                context.Remove(entity);
            }
        }

        public async Task<SCMI> CreatePart(SCMI part)
        {
            using(AMGContext context = _contextFactory.CreateDbContext())
            {
                context.SCMI.Add(part);
                await context.SaveChangesAsync();

                return part;
            }    
        }

        public async Task<SCMI> GetPartNumber(string CustomerId, string PartNumber, string ModelName)
        {
            using (AMGContext context = _contextFactory.CreateDbContext())
            {
                return await context.SCMI.FirstOrDefaultAsync(x =>
                                x.CXCONO == "AMG" &&
                                x.CXBRNO == "CKP" &&
                                x.CXCUNO == CustomerId &&
                                x.CXCUIT == PartNumber &&
                                x.CXITNO == ModelName);
            }
        }

        public async Task<SCMI> Update(SCMI part)
        {
            using (AMGContext context = _contextFactory.CreateDbContext())
            {
                context.SCMI.Update(part);
                await context.SaveChangesAsync();

                return part;
            }
        }

        public async Task<bool> SaveAll()
        {
            using (AMGContext context = _contextFactory.CreateDbContext())
            {
                return await context.SaveChangesAsync() > 0;
            }
        }
    }
}