using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TRAINING.API.Model;
using TRAINING.API.Helper;

namespace TRAINING.API.Data
{
    public interface IWarehouseRepository
    {
        AMGContext GetContext();
        IQueryable<IWHS> Query();
        Task<PagedList<IWHS>> All(WarehouseParams warehouseParams);
        Task<IWHS> Single(string code);
        Task<bool> Create(IWHS data);
        Task<bool> Update(IWHS data);
        Task<bool> Delete(string code);
        Task<IList<IWHS>> Export(WarehouseParams warehouseParams);
        Task<IList<GCT2>> AllType();
        IQueryable<IWGR> QueryGroup();
        Task<IList<IWGR>> AllGroup(WarehouseParams warehouseParams);
        Task<IWGR> SingleGroup(string code);
        Task<bool> CreateGroup(IWGR data);
        Task<bool> UpdateGroup(IWGR data);
        Task<bool> DeleteGroup(IWGR data);
    }
}