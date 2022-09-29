using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HotChocolate.Data;
using HotChocolate.Types;
using TRAINING.API.Data;
using TRAINING.API.GraphQL;

namespace TRAINING.API.Schema.Queries
{
    [ExtendObjectType(typeof(Query))]
    public class PartNumberQuery
    {
        private readonly ISalesRepository _repo;
        private readonly IMapper _mapper;
        public PartNumberQuery(ISalesRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }

        // public async Task<PartNumberType> GetPartNumber(string customerId, string partNumber)
        // {
        //     var data = await _repo.GetPartNumber(customerId, partNumber);
        //     var result = _mapper.Map<PartNumberType>(data);

        //     return result;
        // }
    }
}