using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TRAINING.API.Model;

namespace TRAINING.API.Data
{
    public interface ISalesRepository
    {
        Task<SCMI> GetPartNumber(string CustomerId, string PartNumber);
    }
}