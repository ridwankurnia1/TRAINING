using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TRAINING.API.Helper;
using TRAINING.API.Model;
using TRAINING.API.ViewModel;

namespace TRAINING.API.Data
{
    public interface IProductionRepository2
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveAll();
        Task<IEnumerable<MDF1>> GetMDF1();
        Task<MDF1> GetMDF1ById(string id);
        Task<PagedList<MDF1>> GetListDefect2Paging(Params prm);
        Task<IEnumerable<MDF0>> GetDefectGroup();
    }
}