using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;
using TRAINING.API.Data;
using TRAINING.API.Model;
using TRAINING.API.Schema.Queries;

namespace TRAINING.API.GraphQL
{
    public class Query
    {
        [UseDbContext(typeof(AMGContext))]
        [UseOffsetPaging(IncludeTotalCount = true, DefaultPageSize = 10)]
        [UseFiltering]
        public IQueryable<PartNumberType> GetListPartNumber([ScopedService] AMGContext context, ClaimsPrincipal claimsPrincipal)
        {
            var user = claimsPrincipal.FindFirstValue(ClaimTypes.Name);

            return context.SCMI
                .Where(x => x.CXCONO == "AMG" && x.CXBRNO == "CKP")
                .OrderBy(x => x.CXCUNO)
                .ThenBy(x => x.CXCUIT)
                .ThenBy(x => x.CXITNO)
                .Select(s => new PartNumberType()
            {
                PartNumber = s.CXCUIT,
                PartDescription = s.CXITNA,
                CustomerId = s.CXCUNO,
                ModelName = s.CXITNO,
                AlcNumber = s.CXALCN,
                EoNumber = s.CXEONO,
                Status = s.CXRCST,
                Remarks = s.CXREMA,
                CreatedBy = s.CXCRUS,
                UpdatedBy = user
            });
        }
        // public async Task<IEnumerable<SCMI>> GetPartNumberList([Service] AMGContext context)
        // {
        //     return await context.SCMI.Take(10).ToListAsync();
        // }
    }
}