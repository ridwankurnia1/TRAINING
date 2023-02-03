using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TRAINING.API.Helper;
using TRAINING.API.Model;
using TRAINING.API.ViewModel;

namespace TRAINING.API.Data
{
    public interface IDefectMappingRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveAll();
        Task<IEnumerable<MDMP>> GetMDMP();
        // Task<PagedList<MDMP>> GetMdmpPaging(ProductionParams prm);
        Task<PagedList<MDF1>> GetMdmpPaging(DefectMappingParams prm);
        Task<MDMP> GetMdmpById(string id);
        Task<MDF1> GetMDF1ByDefectType(string defectType);
        Task<MDF1> GetMDF1ByDefectCode(string defectCode);
        Task<IEnumerable<GCT2>> GetLineProcessGroup();
        Task<IEnumerable<ZVAR>> GetZvarDefectType();
        Task<IEnumerable<MDF1>> GetDefectCode();
        
    }
}