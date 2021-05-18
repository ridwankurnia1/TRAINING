using System;
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
        Task<ELOH> GetTapHeader(int id);
        Task<IEnumerable<ELOH>> GetListTapHeader();
        Task<ELOG> GetTapLog(int id);
        Task<int> GetTapLogCount(DateTime dt);
        Task<PagedList<ELOG>> GetListTapLog(Params prm);
        Task<EHAL> GetEmployee(string nik, string rfid);
        Task<IEnumerable<EHAL>> GetListEmployee(Params prm);
        Task<PagedList<EHAL>> GetListEmployeePaging(Params prm);
        Task<IEnumerable<LebaranSummaryDto>> GetSummaryLebaran(string location);
        Task<IEnumerable<DropdownDto>> GetDepartment(string location);
    }
}