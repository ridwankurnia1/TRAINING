using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotChocolate.Data.Filters;
using TRAINING.API.Schema.Queries;

namespace TRAINING.API.Schema.Filters
{
    public class PartFilterType : FilterInputType<PartNumberType>
    {
        protected override void Configure(IFilterInputTypeDescriptor<PartNumberType> descriptor)
        {
            descriptor.BindFieldsExplicitly();
            descriptor.Field(f => f.ModelName).Name("CXITNO");
            descriptor.Field(f => f.PartNumber).Name("CXCUIT");
            descriptor.Field(f => f.PartDescription).Name("CXITNA");
            descriptor.Field(f => f.Status).Name("CXRCST");
            descriptor.Field(f => f.CustomerId).Name("CXCUNO");
            descriptor.Field(f => f.EoNumber).Name("CXEONO");

            base.Configure(descriptor);
        }
    }
}