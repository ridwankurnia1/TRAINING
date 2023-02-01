using System.Collections.Generic;
using System.Threading.Tasks;
using TRAINING.API.Helper;
using TRAINING.API.Model;
using TRAINING.API.ViewModel;

namespace TRAINING.API.Data
{
    public interface IPalletTypeRepository
    {
        System.Linq.IQueryable<IPTY> Query();
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveAll();
        Task<PagedList<IPTY>> All(PalletTypeParams param);
        Task<IList<IPTY>> Exportable(PalletTypeParams param);
        Task<IPTY> FindByUser(string user);
        Task<IPTY> FindByType(string id);
        Task<bool> Update(string type, IPTY data);
        Task<IList<ZVAR>> GetPalletAppDefinition();
        Task<IList<GCT2>> GetCommonnText2(string type);
        Task<IList<GCUR>> GetCurrencyDefinition();
        Task<IList<IUOM>> GetMeasurements();
    }
}