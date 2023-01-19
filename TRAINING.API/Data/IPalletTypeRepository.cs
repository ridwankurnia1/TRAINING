using System.Threading.Tasks;
using TRAINING.API.Helper;
using TRAINING.API.Model;
using TRAINING.API.ViewModel;

namespace TRAINING.API.Data
{
    public interface IPalletTypeRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveAll();
        Task<PagedList<IPTY>> GetAllPalletTypes(PalletTypeParams param);
        Task<IPTY> FindByUser(string user);
        Task<IPTY> FindByType(string id);
        Task<bool> Update(string type, PalletTypeDto data);
    }
}