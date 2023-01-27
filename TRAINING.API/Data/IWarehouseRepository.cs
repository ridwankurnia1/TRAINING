using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TRAINING.API.Model;
using TRAINING.API.Helper;

namespace TRAINING.API.Data
{
    public interface IWarehouseRepository
    {
        IQueryable<IWHSX> Query();
        Task<PagedList<IWHSX>> All(WarehouseParams warehouseParams);
        Task<IWHSX> Single(string code);
        Task<bool> Create(IWHSX data);
        Task<bool> Update(IWHSX data);
        Task<bool> Delete(string code);
        Task<IList<GCT2>> AllType();
        IQueryable<IWGRX> QueryGroup();
        Task<IList<IWGRX>> AllGroup();
        Task<IWGRX> SingleGroup(string code);
        Task<bool> CreateGroup(IWGRX data);
        Task<bool> UpdateGroup(IWGRX data);
        Task<bool> DeleteGroup(IWGRX data);
    }
}