using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HotChocolate;
using HotChocolate.Data;
using Microsoft.EntityFrameworkCore;
using TRAINING.API.Data;
using TRAINING.API.Helper;
using TRAINING.API.Model;
using TRAINING.API.Schema.Mutations;

namespace TRAINING.API.Schema.Mutation
{
    public class Mutation
    {
        private readonly IMapper _mapper;
        private readonly ISalesRepository _repo;

        public Mutation(IMapper mapper, ISalesRepository repo)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<string> CreatePart(PartTypeInput partInput)
        {
            var scmi = await _repo.GetPartNumber(partInput.CustomerId, partInput.PartNumber, partInput.ModelName);
            if (scmi != null)
            {
                if (scmi.CXRCST == 1)
                {
                    throw new GraphQLException(new Error("Already Exists", "SAVE_FAILED"));
                }
                
                scmi.CXRCST = 1;
                scmi.CXCHDT = CommonMethod.DateToNumeric(DateTime.Now);
                scmi.CXCHTM = CommonMethod.TimeToNumeric(DateTime.Now);
                var result = await _repo.Update(scmi);
                if (result != null)
                {
                    return "updated";
                }
            }
            else
            {
                scmi = _mapper.Map<SCMI>(partInput);
                var res = await _repo.CreatePart(scmi);
                return "created";
            }
            
            throw new GraphQLException(new Error("Create data failled", "SAVE_FAILED"));
        }

        public async Task<bool> UpdatePart(PartTypeInput partInput)
        {
            var scmi = await _repo.GetPartNumber(partInput.CustomerId, partInput.PartNumber, partInput.ModelName);

            if (scmi == null)
            {
                throw new GraphQLException(new Error("Data not found", "DATA_NOT_FOUND"));
            }

            scmi.CXALCN = partInput.AlcNumber;
            scmi.CXCHDT = CommonMethod.DateToNumeric(DateTime.Now);
            scmi.CXCHTM = CommonMethod.TimeToNumeric(DateTime.Now);

            var result = await _repo.Update(scmi);
            if (result != null)
                return true;
            
            return false;
        }

        public async Task<bool> DeletePart(PartTypeInput partInput)
        {
            var scmi = await _repo.GetPartNumber(partInput.CustomerId, partInput.PartNumber, partInput.ModelName);

            if (scmi == null)
            {
                throw new GraphQLException(new Error("Data not found", "DATA_NOT_FOUND"));
            }

            scmi.CXRCST = 0;
            scmi.CXCHDT = CommonMethod.DateToNumeric(DateTime.Now);
            scmi.CXCHTM = CommonMethod.TimeToNumeric(DateTime.Now);
            var result = await _repo.Update(scmi);

            if (result != null)
            {
                return true;
            }

            return false;
        }
    }
}