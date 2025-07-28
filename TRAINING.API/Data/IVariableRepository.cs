// Repositories/IVariableRepository.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using TRAINING.API.Helper;
using TRAINING.API.Helpers;
using TRAINING.API.Model;

namespace TRAINING.API.Repositories
{
    public interface IVariableRepository
    {
        Task<PagedList<ZVAR>> GetListVariable(VariableParams prm);
        Task<IEnumerable<ZVAR>> GetAllAsync(VariableParams variableParams);
        Task<ZVAR> GetByIdAsync(int id);
        Task<ZVAR> CreateAsync(ZVAR variable);
        Task<ZVAR> UpdateAsync(int id, ZVAR variable);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}