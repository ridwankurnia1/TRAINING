using System.Collections.Generic;
using System.Threading.Tasks;
using TRAINING.API.Helper;
using TRAINING.API.Model;
using TRAINING.API.ViewModel;

namespace TRAINING.API.Data
{
    public interface ICheckSheetRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveAll();

        Task<EHAL> GetEmployee(string nik, string rfid);
        Task<IEnumerable<EHAL>> GetListEmployee(Params prm);
        Task<PagedList<EHAL>> GetListEmployeePaging(Params prm);
        Task<IEnumerable<LebaranSummaryDto>> GetSummaryLebaran(string location);
    }
}