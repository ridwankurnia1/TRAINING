using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TRAINING.API.Model;

namespace TRAINING.API.Data
{
    public interface ISalesRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveAll();
        Task<SCMI> GetPartNumber(string CustomerId, string PartNumber, string ModelName);
        Task<SCMI> Update(SCMI part);
        Task<SCMI> CreatePart(SCMI part);
    }
}