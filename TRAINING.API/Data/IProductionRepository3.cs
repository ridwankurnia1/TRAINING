using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TRAINING.API.Helper;
using TRAINING.API.Model;
using TRAINING.API.ViewModel;

namespace TRAINING.API.Data
{
    public interface IProductionRepository3
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveAll();
        Task<IEnumerable<MDMP>> GetMDMP();
        Task<PagedList<MDMP>> GetMdmpPaging(Params prm);
        Task<MDMP> GetMdmpById(string id);
        Task<MDF1> GetMDF1ByDefectType(string defectType);
        
    }
}