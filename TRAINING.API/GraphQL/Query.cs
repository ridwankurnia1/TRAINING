using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotChocolate;
using Microsoft.EntityFrameworkCore;
using TRAINING.API.Data;
using TRAINING.API.Model;

namespace TRAINING.API.GraphQL
{
    public class Query
    {
        public async Task<IEnumerable<SCMI>> GetPartNumber([Service] AMGContext context)
        {
            return await context.SCMI.Take(10).ToListAsync();
        }
    }
}