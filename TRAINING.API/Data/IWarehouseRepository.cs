using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TRAINING.API.Data
{
    public interface IWarehouseRepository
    {
        IQueryable<IWGRX> Query();
        Task<IList<IWGRX>> AllGroup();
        Task<IWGRX> SingleGroup(string code);
        Task<bool> CreateGroup(IWGRX data);
        Task<bool> UpdateGroup(IWGRX data);
        Task<bool> DeleteGroup(IWGRX data);
    }
}