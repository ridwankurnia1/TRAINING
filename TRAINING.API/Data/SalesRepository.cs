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

        public SalesRepository(IDbContextFactory<AMGContext> context)
        {
            _contextFactory = context;
        }

        public async Task<SCMI> GetPartNumber(string CustomerId, string PartNumber)
        {
            using (AMGContext context = _contextFactory.CreateDbContext())
            {
                return await context.SCMI.FirstOrDefaultAsync(x =>
                                x.CXCONO == "AMG" &&
                                x.CXBRNO == "CKP" &&
                                x.CXCUNO == CustomerId &&
                                x.CXCUIT == PartNumber &&
                                x.CXRCST == 1);
            }
            
        }
    }
}