using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TRAINING.API.Helper;
using TRAINING.API.Model;
using TRAINING.API.ViewModel;

namespace TRAINING.API.Data
{
    public interface IProductionRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveAll();
        Task<IEnumerable<MDF0>> GetListMDF0();
        Task<PagedList<MDF0>> GetListDefectPaging(InventoryParams prm);
        Task<MDF0> Get1MDF0(int ddtrid); //get nilai dari MDF0 berdasarkan ddtrid
        Task<MDF0> GetNameMDF0(string ddchus); //get nilai dari MDF0 berdasarkan nama
        Task<IEnumerable<MDF0>> FindListMDF0();
        Task<MDF0> GetMDF0ByParams(int id);
        Task<MDF0> GetMDF0ByParamsName(string name);

        Task<MDF0> GetMDF0ByDdtrid(int ddtrid);

        Task<IEnumerable<MDF0>> GetMDF0ByDddfgr(string dddfgr);

        Task<MDF0> GetDfGMDF0(string dddfgr);

        Task<IEnumerable<MDF0>> GetMdf0By(string DefectGroup, int transactionId);


        // Truck
        Task<PagedList<TRCK>> GetListTruck(TruckParams prm);
        Task<TRCK> GetTruck(int truckId);
    Task<IList<TRCK>> Export(TruckParams truckParams);
    }
}