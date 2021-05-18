using TRAINING.API.Model;
using TRAINING.API.Helper;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TRAINING.API.Data
{
    public interface IUserRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveAll();

        Task<MEMP> GetEmployee(string nik);
        Task<PagedList<MEMP>> GetListEmmployee(Params prm);
        Task<IEnumerable<GOG1>> GetOrganization();
        Task<IEnumerable<MGRD>> GetListGrade();
        // Task<List<EMPLOYEE>> GetListEmmployeeAPRISE(Params prm);
        Task<MEMP> GetEmployee(string nik, string rfid);
    }
}